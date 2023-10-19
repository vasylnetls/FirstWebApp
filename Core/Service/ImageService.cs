using Core.Interfaces.Repository;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace Core.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<byte[]> GetImage(int? id, string? name)
        {
            if (!string.IsNullOrEmpty(name) && id.HasValue && id.Value != 0)
            {
                var imageName = id + "_image_" + name;

                if (ImageCache.IsImageDownload(imageName))
                {
                    return ImageCache.GetImages(imageName);
                }

                if (IsImageInBase(imageName, out var dbImage))
                {
                    if (DateTime.Now - dbImage.UpdateDate > TimeSpan.FromMinutes(5))
                    {
                        var bytes = await GetImageFromSite(id, name);
                        if (bytes.Length > 0)
                        {
                            ImageCache.AddImage(imageName, bytes);
                            _imageRepository.UpdateImage(new Entities.Image()
                            {
                                Name = imageName,
                                Data = bytes,
                                UpdateDate = DateTime.Now
                            });
                            
                            return bytes;
                        }
                    }
                    else
                    {
                        ImageCache.AddImage(imageName, dbImage.Data);
                        return dbImage.Data;
                    }
                }
                else
                { 
                    var bytes = await GetImageFromSite(id, name);
                    ImageCache.AddImage(imageName, bytes);
                    _imageRepository.CreateImage(new Entities.Image()
                    {
                        Name = imageName,
                        Data = bytes,
                        UpdateDate = DateTime.Now
                    });
                    
                    return bytes;
                }
            }

            return Array.Empty<byte>();
        }

        public bool IsImageInBase(string name, out IImage? dbImage)
        {
            dbImage = _imageRepository.GetImageByName(name);
            return dbImage != null;
        }

        public async Task<byte[]> GetImageFromSite(int? id, string? name)
        {
            using HttpClient client = new HttpClient();

            try
            {
                using var response = await client.GetAsync($"https://i.dummyjson.com/data/products/{id}/{name}");

                if (response != null && response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {

            }

            return Array.Empty<byte>();
        }
    }
}
