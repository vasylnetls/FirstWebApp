using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using static Core.ImageCache;

namespace Core
{
    public class ImageCache
    {
        private static Dictionary<string, CacheElement<IImage>> images;

        private static readonly object lockObj = new ();

        static ImageCache()
        {
            images = new Dictionary<string, CacheElement<IImage>>();
        }

        public static byte[] GetImages(string key)
        {
            return images.GetValueOrDefault(key, null).Value.Data ?? Array.Empty<byte>();
        }

        public static void AddImage(string key, IImage image, TimeSpan elapsedTime)
        {
            if (images.ContainsKey(key)) return;
            lock (lockObj)
            {
                images.Add(key, new CacheElement<IImage> { Value = image, ElapsedTime = DateTime.Now.Add(elapsedTime)});
            }
        }

        public static bool IsImageDownload(string key)
        {
            if (!images.ContainsKey(key)) return false;
            
            var cacheElement = images.GetValueOrDefault(key, null);
            if (DateTime.Now < cacheElement?.ElapsedTime) return true;
            images.Remove(key);
            return false;
        }

        public class CacheElement<T>
        {
            public T Value { get; set; }
            public DateTime ElapsedTime { get; set; }
        }
    }
}