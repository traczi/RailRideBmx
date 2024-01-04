using Core.Domain.Entity;
using Core.Ports;

namespace Application.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeService(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }
    
    public async Task LikeProductAsync(Guid userId, Guid productId)
    {
        await _likeRepository.LikeProductAsync(userId, productId);
    }

    public async Task UnLikeProductAsync(Guid userId, Guid productId)
    {
        await _likeRepository.UnLikeProductAsync(userId, productId);
    }

    public async Task<List<Product>> GetLikeProductAsync(Guid userId)
    {
        return await _likeRepository.GetLikeProductAsync(userId);
    }

    public async Task<bool> IsProductLikedByUserAsync(Guid userId, Guid productId)
    {
        return await _likeRepository.IsProductLikedByUserAsync(userId, productId);
    }
}