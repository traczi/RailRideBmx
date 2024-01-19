using Application;
using Application.IServices;
using Application.Models.Product;
using Application.Services;
using Core.Domain.Entity;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class ProductController : ApiController
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProductAsync()
    {
        var product = await _productService.GetAllProductAsync();
        return Ok(product);
    }
    
    [HttpGet]
    [Route("{guid}")]
    public async Task<IActionResult> GetProductByIdAsync(Guid guid)
    {
        var product = await _productService.GetProductByIdAsync(guid);
        return Ok(product);
    }

    [HttpGet]
    [Route("category/{category}")]
    public async Task<IActionResult> GetProductByCategorieAsync(string category)
    {
        var product = await _productService.GetProductByCategorieAsync(category);
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductResponseModel productResponseModel, string url)
    {
        var createProduct = await _productService.CreateProductAsync(productResponseModel, url);
        return Ok(createProduct);
    }
    
    [HttpPut]
    [Route("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct(Guid guid, ProductResponseModel productResponseModel)
    {
        var updateProduct = await _productService.UpdateProduct(guid, productResponseModel);
        return Ok(updateProduct);
    }
    
    [HttpDelete]
    [Route("DeleteProduct")]
    public async Task<IActionResult> DeleteProduct(Guid guid)
    {
        var deleteProduct = await _productService.DeleteProduct(guid);
        return Ok(deleteProduct);
    }
    
    [HttpGet]
    [Route("Pagination")]
    public async Task<IActionResult> GetProductsPageAsync(int page = 1, int pageSize = 1)
    {
        try
        {
            var products = await _productService.GetProductPageAsync(page, pageSize);
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("Filter")]
    public async Task<IActionResult> GetProductsFilteredAsync(
        string? searchItem = null,
        int page = 1,
        int pageSize = 30,
        string? color = null,
        string? brand = null,
        float? frameSize = null,
        float? handlebarSize = null,
        float? wheelSize = null,
        bool showInStockOnly = false)
    {
        try
        {
            var products = await _productService.GetProductFilterAndSearchAsync(
                searchItem,
                page,
                pageSize,
                color,
                brand,
                frameSize,
                handlebarSize,
                wheelSize,
                showInStockOnly);

            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Une erreur s'est produite : {ex.Message}");
        }
    }
    
    [HttpGet]
    [Route("GetAllProductProperties")]
    public async Task<IActionResult> GetAllProductProperties()
    {
        var productProperties = await _productService.GetAllProductPropertiesAsync();
        return Ok(productProperties);
    }

    [HttpGet]
    [Route("GetRandomProduct")]
    public async Task<IActionResult> GetRandomProducts()
    {
        var ramdomProducts = await _productService.GetRandomProductAsync();
        return Ok(ramdomProducts);
    }
    
    [HttpGet]
    [Route("top-rated")]
    public async Task<ActionResult<List<ProductDto>>> GetTopRated()
    {
        var topRatedProducts = await _productService.GetTopRatedProductsAsync();
        return Ok(topRatedProducts);
    }
}