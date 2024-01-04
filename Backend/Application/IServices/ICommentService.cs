using Core.Domain.DTOs;
using Core.Domain.Entity;

namespace Application;

public interface ICommentService
{
    public Task AddCommentAsync(Guid productId, Guid userId,int rating, string commentText);
    public Task<List<CommentDto>> GetCommentByProductIdAsync(Guid productId);
    public Task<bool> DeleteUserComment(Guid commentId, Guid userId);
}