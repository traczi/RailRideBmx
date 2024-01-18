using Core.Domain.Entity;
using Core.DTOs;

namespace Application.IServices;

public interface ILikeService
{
    public Task LikeProductAsync(Guid userId, Guid productId);
    public Task UnLikeProductAsync(Guid userId, Guid productId);
    public Task<List<ProductDto>> GetLikeProductAsync(Guid userId);
    public Task<bool> IsProductLikedByUserAsync(Guid userId, Guid productId);
}