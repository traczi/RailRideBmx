using Core.Domain.Entity;

namespace Core.Ports;

public interface IProductRepository
{
    Task<List<Product>> GetProductAsync();
    Task<List<Product>> GetProductByCategorieAsync(string type);
    Task<Product> GetProductByIdAsync(Guid productById);
    Task<Product> UpdateProduct(Product product);
    Task<Product> CreateProduct(Product product);
    Task<Product> DeleteProduct(Product product);

}