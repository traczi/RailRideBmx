using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface ICommentRepository
{
    public Task AddCommentAsync(Comment comment);
    public Task<List<Comment>> GetCommentByProductIdAsync(Guid productId);
    public Task<Comment> GetCommentById(Guid commentId);
    public Task<Comment> DeleteUserComment(Comment comment);
    public Task<double> GetAverageRatingAsync(Guid productId);
    public Task ReportedCommentAsync(Guid commentId);
    public Task<List<Comment>> GetReportedCommentAsync();
    public Task UpdateCommentAsync(Comment comment);

}