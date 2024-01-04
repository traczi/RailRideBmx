using Core.Domain.Entity;

namespace Core.Ports;

public interface ICommentRepository
{
    public Task AddCommentAsync(Comment comment);
    public Task<List<Comment>> GetCommentByProductIdAsync(Guid productId);
    public Task<Comment> GetCommentById(Guid commentId);
    public Task<Comment> DeleteUserComment(Comment comment);

}