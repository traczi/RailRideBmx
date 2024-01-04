namespace Core.Domain.Entity;

public class Like
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}