using Core.Domain.Entity;

namespace Core.Ports;

public interface ICartRepository
{
     Task<Cart> GetOrCreateCartForSessionAsync(string sessionId);
     Task<Cart> GetOrCreateCartForUserAsync(string userId);
     Task AddProductToCartAsync(Guid cartId, Guid product, int quantity);
     Task TransferCartFromSessionToUserAsync(string sessionId, string userId);
     Task<Cart> GetCartBySessionIdAsync(string sessionId);
     Task<Cart> GetCartByUserIdAsync(string userId);
     Task<List<Product>> GetProductInCartAsync(Guid cartId);
     Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity);


}