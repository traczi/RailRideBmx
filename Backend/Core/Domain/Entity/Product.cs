using System;
using System.Collections.Generic;

namespace Core.Domain.Entity
{
    public class Product 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }

        // Clé étrangère pour Category
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        // Clé étrangère pour Color
        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        // Clé étrangère pour Brand
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        // Autres propriétés
        public string? SubCategory { get; set; }
        public string? ConfigCategory { get; set; }
        public string? Geometry { get; set; } 
        public float? FrameSize { get; set; }
        public float? HandlebarSize { get; set; }
        public float? WheelSize { get; set; }

        public List<ProductCart> ProductCarts { get; set; }
        public ICollection<Like> Like { get; set; }
        public ICollection<Comment> Comment { get; set; }

        public Product()
        {
            Comment = new List<Comment>();
        }
    }
}