using Application.Models.Product;
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
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color, 
                Height = product.Height,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Type = product.Type
            };
            
            productsWithImage.Add(productResponse);
        }

        return productsWithImage;
    }

    public async Task<IEnumerable<ProductResponseModel>> GetProductByCategorieAsync(string type)
    {
        var products = await _productRepository.GetProductByCategorieAsync(type);
        var productsWithImage = new List<ProductResponseModel>();

        foreach (var product in products)
        {
            var url = _imageService.ImageUrl(product.Image);

            var productResponse = new ProductResponseModel()
            {
                Brand = product.Brand,
                Price = product.Price,
                Color = product.Color,
                Height = product.Height,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Type = product.Type
            };
            
            productsWithImage.Add(productResponse);
        }

        return productsWithImage;
    }
    
    public async Task<ProductResponseModel> GetProductByIdAsync(Guid guid)
    {
        var product = await _productRepository.GetProductByIdAsync(guid);
        var productById = new ProductResponseModel()
        {
            Brand = product.Brand,
            Id = product.Id,
            Price = product.Price
            
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
            Height = productResponseModel.Height,
            Description = productResponseModel.Description,
            Image = idImage,
            Title = productResponseModel.Title,
            Quantity = productResponseModel.Quantity,
            Type = productResponseModel.Type
        };
        var createProduct = await _productRepository.CreateProduct(product);
        return new ProductResponseModel()
        {
            Brand = createProduct.Brand,
            Price = createProduct.Price,
            Color = createProduct.Color,
            Height = createProduct.Height,
            Description = createProduct.Description,
            Image = createProduct.Image,
            Title = createProduct.Title,
            Quantity = createProduct.Quantity,
            Type = createProduct.Type
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