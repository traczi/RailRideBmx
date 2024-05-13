using System.Drawing;
using Application.IServices;
using Application.Models.Product;
using Application.Services;
using CloudinaryDotNet.Actions;
using Core.Domain.Entity;
using Infrastructure.Ports;
using Moq;
using Color = Core.Domain.Entity.Color;

namespace TestUnitaire.RailRideBMX.Tests.Application.Services;

[TestFixture]
public class ProductsServiceTests
{
    private ProductsService _productsService;
    private Mock<IColorRepository> _colorRepositoryMock;
    private Mock<IBrandRepository> _brandRepositoryMock;
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private Mock<IProductRepository> _productRepositoryMock;
    private Mock<IImageService> _imageServiceMock;

    [SetUp]
    public void Setup()
    {
        _colorRepositoryMock = new Mock<IColorRepository>();
        _brandRepositoryMock = new Mock<IBrandRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _productRepositoryMock = new Mock<IProductRepository>();
        _imageServiceMock = new Mock<IImageService>();
        _productsService = new ProductsService(
            _productRepositoryMock.Object,
            _imageServiceMock.Object,
            _colorRepositoryMock.Object,
            _brandRepositoryMock.Object,
            _categoryRepositoryMock.Object
        );
    }

    [Test]
    public async Task CreateProductAsync_Should_Create_Product_With_Valid_Input()
    {
        var productResponseModel = new ProductResponseModel
        {
            Title = "Example Product",
            Description = "This is a sample product description.",
            Image = "example_image.jpg",
            Price = 99.99f,
            Quantity = 10,
            Category = "Example Category",
            Color = "Red",
            Brand = "Example Brand",
            FrameSize = 18.5f,
            HandlebarSize = 25.0f,
            WheelSize = 26.0f,
            SubCategory = "Example Subcategory",
            ConfigCategory = "Example Configuration Category",
            Geometry = "Example Geometry"
        };
        string brand = "cult";

        _imageServiceMock.Setup(service => service.UploadImage(It.IsAny<string>()))
            .Returns("test");
        _brandRepositoryMock.Setup(s => s.GetBrandNameAsync(It.IsAny<String>()))
            .ReturnsAsync(new Brand() { BrandName =  brand});

        _productRepositoryMock.Setup(repo => repo.CreateProduct(It.IsAny<Product>()))
            .ReturnsAsync( new Product
                {
                    Brand = new Brand(),
                    Price = productResponseModel.Price,
                    Color = new Color(),
                    FrameSize = productResponseModel.FrameSize,
                    HandlebarSize = productResponseModel.HandlebarSize,
                    WheelSize = productResponseModel.WheelSize,
                    Description = productResponseModel.Description,
                    Image = productResponseModel.Image,
                    Title = productResponseModel.Title,
                    Quantity = productResponseModel.Quantity,
                    Category = new Category(),
                    SubCategory = productResponseModel.SubCategory,
                    ConfigCategory = productResponseModel.ConfigCategory,
                    Geometry = productResponseModel.Geometry
                });

        var result = await _productsService.CreateProductAsync(productResponseModel, "https://www.powertrafic.fr/wp-content/uploads/2023/04/image-ia-exemple.png");
        
        Assert.IsNotNull(result);
    }
}