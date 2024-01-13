using Core.Domain.Entity;
using Core.Ports;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _context;

    public CommentRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task AddCommentAsync(Comment comment)
    {
        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Comment>> GetCommentByProductIdAsync(Guid productId)
    {
        return await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
    }

    public async Task<Comment> GetCommentById(Guid commentId)
    {
        var commentById = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
        return commentById;
    }

    public async Task<Comment> DeleteUserComment(Comment comment)
    {
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<double> GetAverageRatingAsync(Guid productId)
    {
        return await _context.Comments
            .Where(c => c.ProductId == productId)
            .AverageAsync(c => (double?)c.Rating) ?? 0;
    }

    public async Task ReportedCommentAsync(Guid commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment != null)
        {
            comment.IsReported = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Comment>> GetReportedCommentAsync()
    {
        return await _context.Comments
            .Where(c => c.IsReported)
            .ToListAsync();
    }

    public async Task UpdateCommentAsync(Comment comment)
    {
        _context.Comments.Update(comment);
        await _context.SaveChangesAsync();
    }
}