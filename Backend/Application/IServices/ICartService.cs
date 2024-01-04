using Core.Domain.DTOs;
using Core.Domain.Entity;

namespace Application;

public interface ICartService
{
    Task<Cart> GetOrCreateCartForSessionAsync(string sessionId);
    Task<Cart> GetOrCreateCartForUserAsync(string userId);
    Task AddProductToCartAsync(Guid cartId, Guid productId, int quantity);
    Task<List<ProductDto>> GetProductInCartAsync(string sessionId);
    Task TransferCartFromSessionToUserAsync(string sessionId, string userId);
    Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity);

}