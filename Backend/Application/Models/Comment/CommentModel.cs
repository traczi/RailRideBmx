namespace Application.Models.Comment;

public class CommentModel
{
    public Guid Id { set; get; }
    public Guid UserId { set; get; }
    public Guid ProductId { set; get; }
    public int Rating { set; get; }
    public string CommentText { set; get; }
    public DateTime DatePosted { set; get; }
}