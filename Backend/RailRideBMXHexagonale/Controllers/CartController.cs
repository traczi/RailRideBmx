using Application.Models.Product;
using Application.Services;
using Core.Domain.Entity;
using Core.Ports;
using Microsoft.AspNetCore.Mvc;
using RailRideBMX.Controllers;

namespace RailRideBMXHexagonale.Controllers;

public class CartController : ApiController
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }
    
    [HttpGet]
    [Route("{userId}")]
    public async Task<IActionResult> GetCartUserAsync(Guid userId)
    {
        var cart = await _cartService.GetCartUserAsync(userId);
        if (cart != null)
        {
            return Ok(cart);
        }

        return NotFound();
    }
    
    [HttpPost]
    [Route("{userId}/add")]
    public async Task<IActionResult> AddToCartAsync(Guid userId, Guid productId, int quantity)
    {
        await _cartService.AddToCartAsync(userId, productId, quantity);
        return Ok("Produit ajouté au panier avec succès.");
    }
}