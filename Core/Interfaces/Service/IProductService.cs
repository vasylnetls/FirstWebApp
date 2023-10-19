using Core.Entities;

namespace Core.Service;

public interface IProductService
{ 
    Task<IEnumerable<Product>> GetAllProductAsync();
    Task<IEnumerable<Product>> GetProducts(string searchString);
    Task<Product?> GetProductById(int id);
}