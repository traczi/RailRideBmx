using System.Net.Mime;
using Core.Domain.DTOs;
using Core.Domain.Entity;
using Core.Ports;

namespace Application.Services;

public class CartService : ICartService
{

    private readonly ICartRepository _cartRepository;
    private readonly IImageService _imageService;

    public CartService(ICartRepository cartRepository,IImageService imageService )
    {
        _cartRepository = cartRepository;
        _imageService = imageService;

    }

    public async Task<Cart> GetOrCreateCartForSessionAsync(string sessionId)
    {
        var cart = await _cartRepository.GetOrCreateCartForSessionAsync(sessionId);
        return cart;
    }

    public async Task<Cart> GetOrCreateCartForUserAsync(string userId)
    {

        var cart = await _cartRepository.GetOrCreateCartForUserAsync(userId);
        return cart;
    }

    public async Task AddProductToCartAsync(Guid cartId, Guid productId, int quantity)
    { 
        await _cartRepository.AddProductToCartAsync(cartId, productId, quantity);
    }

    public async Task<List<ProductDto>> GetProductInCartAsync(string sessionId)
    {
        var cart = await _cartRepository.GetCartBySessionIdAsync(sessionId); 
        if (cart == null)
        {
            return new List<ProductDto>();
        }

        var products = await _cartRepository.GetProductInCartAsync(cart.Id);
    
        var productsDto = new List<ProductDto>();

        foreach (var product in products)
        {
            var productCart = cart.ProductCarts.FirstOrDefault(pc => pc.ProductId == product.Id);

            productsDto.Add(new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Image = _imageService.ImageUrl(product.Image),
                Price = product.Price,
                Quantity = product.Quantity,
                CartQuantity = productCart?.Quantity ?? 0 // Si aucun ProductCart n'est trouvé, la quantité est 0
            });
        }

        return productsDto;
    }

    public async Task TransferCartFromSessionToUserAsync(string sessionId, string userId)
    {
        await _cartRepository.TransferCartFromSessionToUserAsync(sessionId, userId);
    }
    public async Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity)
    {
        await _cartRepository.UpdateProductQuantityAsync(cartId, productId, newQuantity);
    }


}