using System.Security.Claims;
using Application;
using Application.Models.Product;
using Application.Services;
using Core.Domain.Entity;
using Core.Domain.Enums;
using Core.Ports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RailRideBMXHexagonale.Middleware;

namespace RailRideBMXHexagonale.Controllers;

public class CartController : ApiController
{
    private readonly ICartService _cartService;
    private const string SessionName = "UserSessionId";

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    [HttpPost]
    [Route("AddProduct")]
    public async Task<IActionResult> AddProductToCart(Guid productId, int quantity)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (string.IsNullOrEmpty(userId))
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            var cartSession = await _cartService.GetOrCreateCartForSessionAsync(sessionId);
            await _cartService.AddProductToCartAsync(cartSession.Id, productId, quantity);
            return Ok();
        }
        else
        {
            var cartUser = await _cartService.GetOrCreateCartForUserAsync(userId);
            Console.WriteLine( "user : " + cartUser.Id + "produit : " +productId);
            await _cartService.AddProductToCartAsync(cartUser.Id, productId, quantity);
            return Ok();
        }
    }
    
    [HttpGet]
    [Route("GetProducts")]
    public async Task<IActionResult> GetProductsInCart()
    {   
        HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
        var products = await _cartService.GetProductInCartAsync(sessionId);
        return Ok(products);
    }
    
    [HttpPost]
    [Route("UpdateProductQuantity")]
    public async Task<IActionResult> UpdateProductQuantity(Guid productId, int newQuantity)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Guid cartId;

        if (!string.IsNullOrEmpty(userId))
        {
            var cartUser = await _cartService.GetOrCreateCartForUserAsync(userId);
            cartId = cartUser.Id;
        }
        else
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            var cartSession = await _cartService.GetOrCreateCartForSessionAsync(sessionId);
            cartId = cartSession.Id;
        }

        await _cartService.UpdateProductQuantityAsync(cartId, productId, newQuantity);
        return Ok();
    }
}