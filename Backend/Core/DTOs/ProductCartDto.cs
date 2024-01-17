namespace Core.DTOs;

public class ProductCartDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Qunatity { get; set; }
    public ProductDto Product { get; set; }
}