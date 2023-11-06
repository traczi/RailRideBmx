using Application.Models.Product;

namespace Application.Services;

public interface IProductService
{
    Task<IEnumerable<ProductResponseModel>> GetAllProductAsync();
    Task<ProductResponseModel> CreateProductAsync(ProductResponseModel productResponseModel, string url);
    Task<ProductResponseModel> GetProductByIdAsync(Guid guid);
    Task<ProductResponseModel> DeleteProduct(Guid guid);
    Task<ProductResponseModel> UpdateProduct(Guid guid,  ProductResponseModel productResponseModel);
}