namespace Core.DTOs;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Image { get; set; }
    public float Price { get; set; }
    public int Quantity { get; set; }
    public int CartQuantity { get; set; }
}