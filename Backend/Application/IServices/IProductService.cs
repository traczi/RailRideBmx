using Application.Models.Product;
using Core.Domain.DTOs;

namespace Application;

public interface IProductService
{
    Task<IEnumerable<ProductResponseModel>> GetAllProductAsync();
    Task<ProductResponseModel> CreateProductAsync(ProductResponseModel productResponseModel, string url);
    Task<IEnumerable<ProductResponseModel>> GetProductByCategorieAsync(string type);
    Task<ProductResponseModel> GetProductByIdAsync(Guid guid);
    Task<ProductResponseModel> DeleteProduct(Guid guid);
    Task<ProductResponseModel> UpdateProduct(Guid guid,  ProductResponseModel productResponseModel);
    Task<IEnumerable<ProductResponseModel>> GetProductPageAsync(int page, int pageSize);
    Task<
        IEnumerable<ProductResponseModel>> GetProductFilterAndSearchAsync(string? searchItem, int page,
        int pageSize,
        string? color,
        string? brand,
        float? frameSize,
        float? handlebarSize,
        float? wheelSize,
        bool showInStockOnly);
    Task<ProductPropertiesDto> GetAllProductPropertiesAsync();
}