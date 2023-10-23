using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    public class ImageRepository : IImageRepository
    {
        protected readonly MyAppDbContext _dbContext;

        public ImageRepository(MyAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CreateImage(IImage image)
        {
            try
            {
                _dbContext.Images.Add(new Models.Image
                {
                    Name = image.Name,
                    Data = image.Data,
                    UpdateDate = image.UpdateDate
                });

                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public bool UpdateImage(IImage image)
        {
            try
            {
                var dbImage = _dbContext.Images.FirstOrDefault(x => x.Name == image.Name);

                if (dbImage == null) return false;
                dbImage.Data = image.Data;
                dbImage.UpdateDate = image.UpdateDate;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }

        public List<IImage> GetImages()
        {
            var images = _dbContext.Images;

            return Enumerable.Cast<IImage>(images).ToList();
        }

        public IImage? GetImageByName(string name)
        {
            return _dbContext.Images.FirstOrDefault(x => x.Name == name);
        }
    }
}