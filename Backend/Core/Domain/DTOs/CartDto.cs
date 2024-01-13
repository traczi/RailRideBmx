using Core.Domain.Entity;

namespace Core.Domain.DTOs;

public class CartDto
{
    public Guid Id { get; set; }
    public List<ProductDto> ProductCarts { get; set; }
}