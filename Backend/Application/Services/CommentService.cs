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
            Id = c.Id,
            UserId = c.UserId,
            CommentText = c.CommentText
        }).ToList();
        return commentDto;
    }

    public async Task DeleteUserComment(Guid commentId, Guid userId, string userRole)
    {
        var comment = await _commentRepository.GetCommentById(commentId);
        if (comment == null)
        {
            throw new InvalidOperationException("Comment not found");
        }

        bool isAdmin = userRole == "Admin";
        if (comment.UserId != userId && !isAdmin)
        {
            throw new InvalidOperationException("User does not have the permission to do this");
        }

        await _commentRepository.DeleteUserComment(comment);
    }

    public async Task<double> CalculateAverageRatingAsync(Guid productId)
    {
        return await _commentRepository.GetAverageRatingAsync(productId);
    }

    public async Task ReportComment(Guid commentId)
    {
        await _commentRepository.ReportedCommentAsync(commentId);
    }

    public async Task<List<Comment>> GetReportedCommentAsync()
    {
        return await _commentRepository.GetReportedCommentAsync();
    }

    public async Task UpdateCommentAsync(Guid commentId, Guid userId, string newCommentText)
    {
        var comment = await _commentRepository.GetCommentById(commentId);
        if (comment == null || comment.UserId != userId)
        {
            throw new InvalidOperationException("Comment not found or user are not enable to do this");
        }

        comment.CommentText = newCommentText;
        await _commentRepository.UpdateCommentAsync(comment);
    }
}