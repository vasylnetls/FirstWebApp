using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Core.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMemoryCache _memoryCache;

        public ProductService(IProductRepository productRepository, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _memoryCache = memoryCache;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts(string searchString)
        {
            var products = await GetAllProductAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(s =>
                    s.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) ||
                    s.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            return products;
        }

        public async Task<Product?> GetProductById(int id)
        {
            var products = await GetAllProductAsync();
            Product? product = null;
            if (0 < id)
            {
                product = products.First(p => p.Id == id);
            }

            return product;
        }
    }
}
