using Core.Entities;

namespace Core.Interfaces.Repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProductAsync();
}