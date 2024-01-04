namespace Core.Domain.Entity;

public class Comment
{
    public Guid Id { set; get; }
    public Guid UserId { set; get; }
    public User User { set; get; }
    public Guid ProductId { set; get; }
    public Product Product { set; get; }
    public int Rating { set; get; }
    public string CommentText { set; get; }
    public DateTime DatePosted { set; get; }
}