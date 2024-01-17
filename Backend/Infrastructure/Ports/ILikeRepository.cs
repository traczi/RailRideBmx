using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface ILikeRepository
{
    public Task LikeProductAsync(Guid userId, Guid productId);
    public Task UnLikeProductAsync(Guid userId, Guid productId);
    public Task<List<Product>> GetLikeProductAsync(Guid userId);
    public Task<bool> IsProductLikedByUserAsync(Guid userId, Guid productId);
}