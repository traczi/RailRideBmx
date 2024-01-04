using System.Runtime.CompilerServices;
using Core.Domain.DTOs;
using Core.Domain.Entity;
using Core.Ports;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }
    
    public async Task AddCommentAsync(Guid productId, Guid userId,int rating, string commentText)
    {
        var comment = new Comment
        {
            ProductId = productId,
            UserId = userId,
            Rating = rating,
            CommentText = commentText,
            DatePosted = DateTime.UtcNow
        };
        await _commentRepository.AddCommentAsync(comment);
    }

    public async Task<List<CommentDto>> GetCommentByProductIdAsync(Guid productId)
    {
        
        var comment = await _commentRepository.GetCommentByProductIdAsync(productId);
        var commentDto = comment.Select(c => new CommentDto
        {
            CommentText = c.CommentText
        }).ToList();
        return commentDto;
    }

    public async Task<bool> DeleteUserComment(Guid commentId, Guid userId)
    {
        var comment = await _commentRepository.GetCommentById(commentId);
        if (comment != null && comment.UserId == userId)
        {
            await _commentRepository.DeleteUserComment(comment);
            return true;
        }

        return false;
    }
}