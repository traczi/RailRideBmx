using Application.Models.Product;
using Core.Domain.DTOs;
using Core.Domain.Entity;
using Core.Ports;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IImageService _imageService;

    public ProductService(IProductRepository productRepository,IImageService imageService )
    {
        _productRepository = productRepository;
        _imageService = imageService;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetAllProductAsync()
    {
        var products = await _productRepository.GetProductAsync();
        var productsWithImage = new List<ProductResponseModel>();

        foreach (var product in products)
        {
            var url = _imageService.ImageUrl(product.Image);

            var productResponse = new ProductResponseModel()
            {
                Id = product.Id,
                Price = product.Price,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity
            };
            
            productsWithImage.Add(productResponse);
        }

        return productsWithImage;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetProductByCategorieAsync(string category)
    {
        var products = await _productRepository.GetProductByCategorieAsync(category);
        var productsWithImage = new List<ProductResponseModel>();

        foreach (var product in products)
        {
            var url = _imageService.ImageUrl(product.Image);

            var productResponse = new ProductResponseModel()
            {
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color,
                FrameSize = product.FrameSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category
            };
            
            productsWithImage.Add(productResponse);
        }

        return productsWithImage;
    }
    
    public async Task<ProductResponseModel> GetProductByIdAsync(Guid guid)
    {
        var product = await _productRepository.GetProductByIdAsync(guid);
        var url = _imageService.ImageUrl(product.Image);
        var productById = new ProductResponseModel()
        {
            Id = product.Id,
            Brand = product.Brand,
            Price = product.Price,
            Color = product.Color, 
            FrameSize = product.FrameSize,
            HandlebarSize = product.HandlebarSize,
            WheelSize = product.WheelSize,
            Description = product.Description,
            Image = url,
            Title = product.Title,
            Quantity = product.Quantity,
            Category = product.Category,
            SubCategory = product.SubCategory,
            Geometry = product.Geometry
            
        };
        return productById;
    }

    public async Task<ProductResponseModel> CreateProductAsync(ProductResponseModel productResponseModel, string url)
    {
        var idImage = _imageService.UploadImage(url);
        var product = new Product()
        {
            Brand = productResponseModel.Brand,
            Price = productResponseModel.Price,
            Color = productResponseModel.Color,
            FrameSize = productResponseModel.FrameSize,
            HandlebarSize = productResponseModel.HandlebarSize,
            WheelSize = productResponseModel.WheelSize,
            Description = productResponseModel.Description,
            Image = idImage,
            Title = productResponseModel.Title,
            Quantity = productResponseModel.Quantity,
            Category = productResponseModel.Category,
            SubCategory = productResponseModel.SubCategory,
            Geometry = productResponseModel.Geometry
        };
        var createProduct = await _productRepository.CreateProduct(product);
        return new ProductResponseModel()
        {
            Brand = createProduct.Brand,
            Price = createProduct.Price,
            Color = createProduct.Color,
            FrameSize = createProduct.FrameSize,
            HandlebarSize = createProduct.HandlebarSize,
            WheelSize = createProduct.WheelSize,
            Description = createProduct.Description,
            Image = createProduct.Image,
            Title = createProduct.Title,
            Quantity = createProduct.Quantity,
            Category = createProduct.Category,
            SubCategory = createProduct.SubCategory,
            Geometry = createProduct.Geometry
        };
    }

    public async Task<ProductResponseModel> UpdateProduct(Guid guid, ProductResponseModel productResponseModel)
    {
        var productId = await _productRepository.GetProductByIdAsync(guid);
        productId.Brand = productResponseModel.Brand;
        productId.Price = productResponseModel.Price;
        var updateProduct = await _productRepository.UpdateProduct(productId);
        return new ProductResponseModel()
        {
            Brand = updateProduct.Brand,
            Id = updateProduct.Id,
            Price = updateProduct.Price
        };
    }

    public async Task<IEnumerable<ProductResponseModel>> GetProductPageAsync(int page, int pageSize)
    {
        var productPage = await _productRepository.GetProductPage(page, pageSize);
        var productsWithImage = new List<ProductResponseModel>();
        foreach (var product in productPage)
        {
            var url = _imageService.ImageUrl(product.Image);
            var productByPage = new ProductResponseModel()
            {
                Id = product.Id,
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color, 
                FrameSize = product.FrameSize,
                HandlebarSize = product.HandlebarSize,
                WheelSize = product.WheelSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category,
                SubCategory = product.SubCategory,
                Geometry = product.Geometry
            
            };
            productsWithImage.Add(productByPage);
        }

        return productsWithImage;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetProductFilterAndSearchAsync(string? searchItem, int page, int pageSize, string? color, string? brand, float? frameSize, float? handlebarSize,
        float? wheelSize, bool showInStockOnly)
    {
        var productFilter = await _productRepository.GetProductFilterAndSearch(searchItem, page, pageSize, color, brand, frameSize, handlebarSize, wheelSize,
            showInStockOnly);
        var productsWithImage = new List<ProductResponseModel>();
        foreach (var product in productFilter)
        {
            var url = _imageService.ImageUrl(product.Image);
            var productByFilter = new ProductResponseModel()
            {
                Id = product.Id,
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color, 
                FrameSize = product.FrameSize,
                HandlebarSize = product.HandlebarSize,
                WheelSize = product.WheelSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category,
                SubCategory = product.SubCategory,
                Geometry = product.Geometry
            
            };
            productsWithImage.Add(productByFilter);
        }

        return productsWithImage;
    }

    public async Task<ProductPropertiesDto> GetAllProductPropertiesAsync()
    {
        return await _productRepository.GetAllProductPropertiesAsync();
    }

    public async Task<ProductResponseModel> DeleteProduct(Guid guid)
    {
        var product = await _productRepository.GetProductByIdAsync(guid);
        var productDelete = await _productRepository.DeleteProduct(product);
        var productModel = new ProductResponseModel()
        {
            Brand = productDelete.Brand,
            Id = productDelete.Id,
            Price = productDelete.Price
        };
        return productModel;
    }
}