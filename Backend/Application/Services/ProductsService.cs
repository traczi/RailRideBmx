using Application.IServices;
using Application.Models.Product;
using Core.Domain.Entity;
using Core.DTOs;
using Infrastructure.Ports;

namespace Application.Services;

public class ProductsService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IImageService _imageService;
    private readonly IColorRepository _colorRepository;
    private readonly IBrandRepository _brandRepository;
    private readonly ICategoryRepository _categoryRepository;

    
    public ProductsService(IProductRepository productRepository,IImageService imageService, IColorRepository colorRepository, IBrandRepository brandRepository, ICategoryRepository categoryRepository )
    {
        _productRepository = productRepository;
        _imageService = imageService;
        _colorRepository = colorRepository;
        _brandRepository = brandRepository;
        _categoryRepository = categoryRepository;
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
                Brand = product.Brand.BrandName,
                Price = product.Price,
                Color = product.Color.ColorName,
                FrameSize = product.FrameSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category.CategoryName,
                ConfigCategory = product.ConfigCategory
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
            Brand = product.Brand.BrandName,
            Price = product.Price,
            Color = product.Color.ColorName, 
            FrameSize = product.FrameSize,
            HandlebarSize = product.HandlebarSize,
            WheelSize = product.WheelSize,
            Description = product.Description,
            Image = url,
            Title = product.Title,
            Quantity = product.Quantity,
            Category = product.Category.CategoryName,
            SubCategory = product.SubCategory,
            ConfigCategory = product.ConfigCategory,
            Geometry = product.Geometry
            
        };
        return productById;
    }

    public async Task<ProductResponseModel> CreateProductAsync(ProductResponseModel productResponseModel, string url)
    {
        var idImage = _imageService.UploadImage(url);

        var color = await _colorRepository.GetColorNameAsync(productResponseModel.Color);
        if (color == null)
        {
            color = new Color { ColorName = productResponseModel.Color };
            await _colorRepository.CreateColorAsync(color);
        }
        
        var brand = await _brandRepository.GetBrandNameAsync(productResponseModel.Brand);
        if (brand == null)
        {
            brand = new Brand { BrandName = productResponseModel.Brand };
            await _brandRepository.CreateBrandAsync(brand);
        }
        
        var category = await _categoryRepository.GetCategoryNameAsync(productResponseModel.Category);
        if (category == null)
        {
            category = new Category { CategoryName = productResponseModel.Category };
            await _categoryRepository.CreateCategoryAsync(category);
        }
        
        var product = new Product()
        {
            Brand = brand,
            Price = productResponseModel.Price,
            Color = color,
            FrameSize = productResponseModel.FrameSize,
            HandlebarSize = productResponseModel.HandlebarSize,
            WheelSize = productResponseModel.WheelSize,
            Description = productResponseModel.Description,
            Image = idImage,
            Title = productResponseModel.Title,
            Quantity = productResponseModel.Quantity,
            Category = category,
            SubCategory = productResponseModel.SubCategory,
            ConfigCategory = productResponseModel.ConfigCategory,
            Geometry = productResponseModel.Geometry
        };
        var createProduct = await _productRepository.CreateProduct(product);
        return new ProductResponseModel()
        {
            Brand = createProduct.Brand.BrandName,
            Price = createProduct.Price,
            Color = createProduct.Color.ColorName,
            FrameSize = createProduct.FrameSize,
            HandlebarSize = createProduct.HandlebarSize,
            WheelSize = createProduct.WheelSize,
            Description = createProduct.Description,
            Image = createProduct.Image,
            Title = createProduct.Title,
            Quantity = createProduct.Quantity,
            Category = createProduct.Category.CategoryName,
            SubCategory = createProduct.SubCategory,
            ConfigCategory = createProduct.ConfigCategory,
            Geometry = createProduct.Geometry
        };
    }

    public async Task<ProductResponseModel> UpdateProduct(Guid guid, ProductResponseModel productResponseModel)
    {
        var productId = await _productRepository.GetProductByIdAsync(guid);
        productId.Brand.BrandName = productResponseModel.Brand;
        productId.Price = productResponseModel.Price;
        var updateProduct = await _productRepository.UpdateProduct(productId);
        return new ProductResponseModel()
        {
            Brand = updateProduct.Brand.BrandName,
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
                Brand = product.Brand.BrandName,
                Price = product.Price,
                Color = product.Color.ColorName, 
                FrameSize = product.FrameSize,
                HandlebarSize = product.HandlebarSize,
                WheelSize = product.WheelSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category.CategoryName,
                SubCategory = product.SubCategory,
                ConfigCategory = product.ConfigCategory,
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
                Brand = product.Brand.BrandName,
                Price = product.Price,
                Color = product.Color.ColorName, 
                FrameSize = product.FrameSize,
                HandlebarSize = product.HandlebarSize,
                WheelSize = product.WheelSize,
                Description = product.Description,
                Image = url,
                Title = product.Title,
                Quantity = product.Quantity,
                Category = product.Category.CategoryName,
                SubCategory = product.SubCategory,
                ConfigCategory = product.ConfigCategory,
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

    public async Task<List<ProductDto>> GetRandomProductAsync()
    {
        var products = await _productRepository.GetRandomProductsAsync(4);
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Quantity = p.Quantity,
            Image = _imageService.ImageUrl(p.Image),
            Price = p.Price
        }).ToList();
    }

    public async Task<ProductResponseModel> DeleteProduct(Guid guid)
    {
        var product = await _productRepository.GetProductByIdAsync(guid);
        var productDelete = await _productRepository.DeleteProduct(product);
        var productModel = new ProductResponseModel()
        {
            Brand = productDelete.Brand.BrandName,
            Id = productDelete.Id,
            Price = productDelete.Price
        };
        return productModel;
    }
    
    public async Task<List<ProductDto>> GetTopRatedProductsAsync(int count = 4)
    {
        var products = await _productRepository.GetTopRatedProductsAsync(count);
        return products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Quantity = p.Quantity,
            Image = _imageService.ImageUrl(p.Image),
            Price = p.Price,
            AverageRating = p.Comment.Any() ? p.Comment.Average(c => c.Rating) : 0
        }).ToList();
    }
}