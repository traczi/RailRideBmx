using Core.Domain.Entity;
using Core.DTOs;

namespace Application.IServices;

public interface ICartService
{
    Task<Cart> GetOrCreateCartForSessionAsync(string sessionId);
    Task<Cart> GetOrCreateCartForUserAsync(string userId);
    Task AddProductToCartAsync(Guid cartId, Guid productId, int quantity);
    Task<List<ProductDto>> GetProductInCartAsync(string sessionId);
    Task<List<ProductDto>> GetProductInCartByUserAsync(string sessionId);
    Task TransferCartFromSessionToUserAsync(string sessionId, string userId);
    Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity);
    Task<List<CartDto>> GetPaidCartsByUserIdAsync(string userId);
    Task<List<CartDto>> GetPaidCartsBySessionIdAsync(string sessionId);
    Task<bool> UpdateCartStatus(Guid cartId);
    Task<Guid> GetCartIdBySessionIdAsync(string sessionId);
    Task<Guid> GetCartIdByUserIdAsync(string userId);
    Task RemoveProductFromCartAsync(Guid cartId, Guid productId);
}