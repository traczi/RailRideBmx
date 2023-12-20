using Core.Domain.Entity;

namespace Application.Services;

public interface ICartService
{
    Task AddToCartAsync(Guid userId, Guid productId, int quantity);
    Task<Cart?> GetCartUserAsync(Guid userId);
}