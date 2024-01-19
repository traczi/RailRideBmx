using System.Security.Claims;
using Application;
using Application.IServices;
using Application.Models.Product;
using Application.Services;
using Core.Domain.Entity;
using Core.Domain.Enums;
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
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            var productsSession = await _cartService.GetProductInCartAsync(sessionId);
            return Ok(productsSession);
        }
        else
        {
            var productsUser = await _cartService.GetProductInCartByUserAsync(userId);
            return Ok(productsUser);
        }
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
    
    [HttpGet]
    [Route("UserCarts/")]
    public async Task<IActionResult> GetPaidCartsByUserId()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            var carts = await _cartService.GetPaidCartsByUserIdAsync(userId);
            return Ok(carts);
            
        }
        else
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            var carts = await _cartService.GetPaidCartsBySessionIdAsync(sessionId);
            return Ok(carts);
        }
    }
    
    [HttpDelete]
    [Route("DeleteProduct")]
    public async Task<IActionResult> RemoveProductFromCart(Guid cartId, Guid productId)
    {
        try
        {
            await _cartService.RemoveProductFromCartAsync(cartId, productId);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("CartId")]
    public async Task<IActionResult> GetCartId()
    {
        var cartId = new Guid();
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        Console.WriteLine("=============" +userId);
        if (string.IsNullOrEmpty(userId))
        {
            HttpContext.Request.Cookies.TryGetValue(SessionName, out var sessionId);
            cartId = await _cartService.GetCartIdBySessionIdAsync(sessionId);
        }
        else
        {
            cartId = await _cartService.GetCartIdByUserIdAsync(userId);
        }
        
        return Ok(cartId);
    }
}