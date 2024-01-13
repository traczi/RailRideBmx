namespace Core.Domain.Entity;

public class Cart
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string SessionId { get; set; }
    public List<ProductCart> ProductCarts { get; set; }
    public bool IsPayd { get; set; }
}