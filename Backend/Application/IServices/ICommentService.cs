using Core.Domain.DTOs;
using Core.Domain.Entity;

namespace Application;

public interface ICommentService
{
    public Task AddCommentAsync(Guid productId, Guid userId,int rating, string commentText);
    public Task<List<CommentDto>> GetCommentByProductIdAsync(Guid productId);
    public Task DeleteUserComment(Guid commentId, Guid userId, string userRole);
    public Task<double> CalculateAverageRatingAsync(Guid productId);
    public Task ReportComment(Guid commentId);
    public Task<List<Comment>> GetReportedCommentAsync();
    public Task UpdateCommentAsync(Guid commentId, Guid userId, string newCommentText);
}