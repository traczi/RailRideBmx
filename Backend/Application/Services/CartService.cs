using Core.Domain.Entity;
using Core.Ports;

namespace Application.Services;

public class CartService : ICartService
{

    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    public  Task AddToCartAsync(Guid userId, Guid productId, int quantity)
    {
        return _cartRepository.AddProductToCartAsync(userId, productId, quantity);
    }

    public Task<Cart?> GetCartUserAsync(Guid userId)
    {
        return _cartRepository.GetCartByUserIdAsync(userId);
    }
}