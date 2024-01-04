using Core.Domain.Entity;
using Core.Ports;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class LikeRepository : ILikeRepository
{
    private readonly ApplicationDbContext _context;

    public LikeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task LikeProductAsync(Guid userId, Guid productId)
    {
        var like = new Like
        {
            UserId = userId,
            ProductId = productId
        };
        _context.Likes.Add(like);
        await _context.SaveChangesAsync();
    }

    public async Task UnLikeProductAsync(Guid userId, Guid productId)
    {
        var like = await _context.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.ProductId == productId);
        if (like != null)
        {
            _context.Likes.Remove(like);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Product>> GetLikeProductAsync(Guid userId)
    {
        return await _context.Likes
            .Where(l => l.UserId == userId)
            .Select(l => l.Product)
            .ToListAsync();
    }

    public async Task<bool> IsProductLikedByUserAsync(Guid userId, Guid productId)
    {
        return await _context.Likes.AnyAsync(l => l.UserId == userId && l.ProductId == productId);
    }
}