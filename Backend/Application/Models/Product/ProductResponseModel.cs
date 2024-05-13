namespace Application.Models.Product
{
    public class ProductResponseModel : BaseResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CategoryId { get; set; } // Remplacer Category par CategoryId
        public string Category { get; set; }
        public string? SubCategory { get; set; }
        public string? ConfigCategory { get; set; }
        public string? Geometry { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid ColorId { get; set; } // Remplacer Color par ColorId
        public string Color { get; set; }
        public Guid BrandId { get; set; } // Remplacer Brand par BrandId
        public string Brand { get; set; }
        public float? FrameSize { get; set; }
        public float? HandlebarSize { get; set; }
        public float? WheelSize { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}