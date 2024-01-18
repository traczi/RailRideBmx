
using Core.Domain.Entity;
using Core.DTOs;

namespace Infrastructure.Ports;

public interface IProductRepository
{
    Task<List<Product>> GetProductAsync();
    Task<List<Product>> GetProductByCategorieAsync(string type);
    Task<Product> GetProductByIdAsync(Guid productById);
    Task<Product> UpdateProduct(Product product);
    Task<Product> CreateProduct(Product product);
    Task<Product> DeleteProduct(Product product);
    Task<List<Product>> GetProductPage(int page, int pageSize);
    Task<List<Product>> GetProductFilterAndSearch(string? searchItem, int page,
        int pageSize,
        string? color,
        string? brand,
        float? frameSize,
        float? handlebarSize,
        float? wheelSize,
        bool showInStockOnly);

    Task<ProductPropertiesDto> GetAllProductPropertiesAsync();
    Task<List<Product>> GetRandomProductsAsync(int count);
    Task<List<Product>> GetTopRatedProductsAsync(int count);

}