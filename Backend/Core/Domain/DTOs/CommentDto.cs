namespace Core.Domain.DTOs;

public class CommentDto
{
    public Guid Id { get; set; }
    public string CommentText { get; set; }
    public Guid UserId { get; set; }
}