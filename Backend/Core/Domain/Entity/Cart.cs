using Core.Entities;

namespace Core.Domain.Entity;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<ProductCart> ProductCarts { get; set; }
    public User User { get; set; }
}
