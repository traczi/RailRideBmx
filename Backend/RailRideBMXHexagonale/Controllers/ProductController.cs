using Application.Models.Product;
using Application.Services;
using Core.Domain.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailRideBMX.Controllers;

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
    
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductResponseModel productResponseModel, string url)
    {
        var createProduct = await _productService.CreateProductAsync(productResponseModel, url);
        return Ok(createProduct);
    }
    
    [HttpPut]
    [Route("{guid}")]
    public async Task<IActionResult> UpdateProduct(Guid guid, ProductResponseModel productResponseModel)
    {
        var updateProduct = await _productService.UpdateProduct(guid, productResponseModel);
        return Ok(updateProduct);
    }
    
    [HttpDelete]
    [Route("{guid}")]
    public async Task<IActionResult> DeleteProduct(Guid guid)
    {
        var deleteProduct = await _productService.DeleteProduct(guid);
        return Ok(deleteProduct);
    }
}