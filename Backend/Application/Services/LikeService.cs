using Application.IServices;
using Core.Domain.Entity;
using Core.DTOs;
using Infrastructure.Ports;

namespace Application.Services;

public class LikeService : ILikeService
{
    private readonly ILikeRepository _likeRepository;
    private readonly IImageService _imageService;

    public LikeService(ILikeRepository likeRepository, IImageService imageService)
    {
        _likeRepository = likeRepository;
        _imageService = imageService;
    }
    
    public async Task LikeProductAsync(Guid userId, Guid productId)
    {
        await _likeRepository.LikeProductAsync(userId, productId);
    }

    public async Task UnLikeProductAsync(Guid userId, Guid productId)
    {
        await _likeRepository.UnLikeProductAsync(userId, productId);
    }

    public async Task<List<ProductDto>> GetLikeProductAsync(Guid userId)
    {
        var products = await _likeRepository.GetLikeProductAsync(userId);
        var productDto = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Title = p.Title,
            Image = _imageService.ImageUrl(p.Image),
            Price = p.Price,
            Quantity = p.Quantity,
        }).ToList();
        return productDto;
    }

    public async Task<bool> IsProductLikedByUserAsync(Guid userId, Guid productId)
    {
        return await _likeRepository.IsProductLikedByUserAsync(userId, productId);
    }
}