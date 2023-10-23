using Core.Entities;
using Core.Interfaces.Repository;
using Core.Interfaces;
using Microsoft.Extensions.Options;

namespace Core.Service
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly AppConf _options;

        public ImageService(IImageRepository imageRepository, IOptions<AppConf> options)
        {
            _options = options.Value;
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

                if (!IsImageInBase(imageName, out var dbImage))
                    return await UpdateImage(_imageRepository.CreateImage, id, name, Array.Empty<byte>());
                if (DateTime.Now - dbImage.UpdateDate > _options.ElapsedTime)
                {
                    return await UpdateImage(_imageRepository.UpdateImage, id, name, dbImage.Data);
                }

                ImageCache.AddImage(imageName, dbImage, _options.ElapsedCacheTime);
                return dbImage.Data;
            }

            return Array.Empty<byte>();
        }

        private async Task<byte[]> UpdateImage(Func<IImage, bool> func, int? id, string? name, byte[]? defaultData)
        {
            var imageBytes = await GetImageFromSite(id, name);
            if (imageBytes.Length <= 0) return defaultData;

            var imageName = GetImageName(id, name);
            var image = new Image()
            {
                Data = imageBytes,
                Name = imageName,
                UpdateDate = DateTime.Now
            };
            ImageCache.AddImage(imageName, image, _options.ElapsedCacheTime);
            func(image);

            return imageBytes;
        }

        private string GetImageName(int? id, string? name) => id + "_image_" + name;

        public bool IsImageInBase(string name, out IImage? dbImage)
        {
            dbImage = _imageRepository.GetImageByName(name);
            return dbImage != null;
        }

        public async Task<byte[]> GetImageFromSite(int? id, string? name)
        {
            using var client = new HttpClient();

            try
            {
                using var response = await client.GetAsync($"https://i.dummyjson.com/data/products/{id}/{name}");

                if (response is { IsSuccessStatusCode: true })
                {
                    return await response.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Array.Empty<byte>();
        }
    }
}