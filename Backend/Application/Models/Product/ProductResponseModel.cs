
namespace Application.Models.Product;

public class ProductResponseModel : BaseResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public int Quantity { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public string Color { get; set; }
    public float Height { get; set; }
    public string Brand { get; set; }
    public float Price { get; set; }
}