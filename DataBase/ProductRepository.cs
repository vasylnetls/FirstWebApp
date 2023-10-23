using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repository;
using DataBase.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DataBase
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMemoryCache _memoryCache;
        private const string productKey = "productKey";
        
        public ProductRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            if (_memoryCache.TryGetValue(productKey, out IEnumerable<Product>? data)) return data;
            using var client = new HttpClient();
            using var response = await client.GetAsync("https://dummyjson.com/products?limit=100");

            var str = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ProductDb>(str, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            data = result?.Products;

            _memoryCache.Set(productKey, data, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return data;
        }
    }
}