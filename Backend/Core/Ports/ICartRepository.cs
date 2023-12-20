using Core.Domain.Entity;

namespace Core.Ports;

public interface ICartRepository
{
     Task<Cart?> GetCartByUserIdAsync(Guid userId);
     Task AddProductToCartAsync(Guid userId, Guid productId, int quantity);
}