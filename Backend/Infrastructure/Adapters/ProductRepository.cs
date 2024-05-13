using Core.Domain.Entity;
using Core.DTOs;
using Infrastructure.DbContext;
using Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Product>> GetProductAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>> GetProductByCategorieAsync(string category)
    {
        var productByCategory = _context.Products.Where(x => x.Category.CategoryName == category).ToListAsync();
        return await productByCategory;
    }
    public async Task<Product> GetProductByIdAsync(Guid productId)
    {
        var productById = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        return productById;
    }

    public async Task<Product> UpdateProduct(Product product)
    {
        var existingBmx = await _context.Products.FindAsync(product.Id);
        if (existingBmx != null)
        {
            _context.Entry(existingBmx).State = EntityState.Detached;
        }
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<Product> DeleteProduct(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<List<Product>> GetProductPage(int page, int pageSize)
    {
        var productPage = await _context.Products
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return productPage;
    }

    public async Task<List<Product>> GetProductFilterAndSearch(string? searchItem, int page, int pageSize, string? color, string? brand, float? frameSize, float? handlebarSize,
        float? wheelSize, bool showInStockOnly)
    {
        var query = _context.Products.AsQueryable();
        if (!string.IsNullOrEmpty(searchItem))
        {
            query = query.Where(p => p.Title.Contains(searchItem));
        }
        
        if (!string.IsNullOrEmpty(color))
        {
            query = query.Where(p => p.Color.ColorName == color);
        }
        
        if (!string.IsNullOrEmpty(brand))
        {
            query = query.Where(p => p.Brand.BrandName == brand);
        }
        
        if (frameSize.HasValue)
        {
            query = query.Where(p => p.FrameSize == frameSize);
        }
        
        if (handlebarSize.HasValue)
        {
            query = query.Where(p => p.HandlebarSize == handlebarSize);
        }
        
        if (wheelSize.HasValue)
        {
            query = query.Where(p => p.WheelSize == wheelSize);
        }

        if (showInStockOnly)
        {
            query = query.Where(p => p.Quantity > 0);
        }

        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<ProductPropertiesDto> GetAllProductPropertiesAsync()
    {
        var productPropertiesDto = new ProductPropertiesDto
        {
            Color = await _context.Products
                .Where(p => !string.IsNullOrWhiteSpace(p.Color.ColorName))
                .Select(p => p.Color.ColorName)
                .Distinct()
                .ToListAsync(),
            Brand = await _context.Products
                .Where(p => !string.IsNullOrWhiteSpace(p.Brand.BrandName))
                .Select(p => p.Brand.BrandName)
                .Distinct()
                .ToListAsync(),
            FrameSize = await _context.Products
                .Where(p => p.FrameSize.HasValue)
                .Select(p => p.FrameSize)
                .Distinct()
                .ToListAsync(),
            HandlebarSize = await _context.Products
                .Where(p => p.HandlebarSize.HasValue)
                .Select(p => p.HandlebarSize)
                .Distinct()
                .ToListAsync(),
            WheelSize = await _context.Products
                .Where(p => p.WheelSize.HasValue)
                .Select(p => p.WheelSize)
                .Distinct()
                .ToListAsync(),
        };
        return productPropertiesDto;
    }

    public async Task<List<Product>> GetRandomProductsAsync(int count)
    {
        return await _context.Products.OrderBy(r => Guid.NewGuid())
            .Take(count)
            .ToListAsync();
    }
    
    public async Task<List<Product>> GetTopRatedProductsAsync(int count)
    {
        var topRatedProducts = await _context.Products
            .Select(p => new
            {
                Product = p,
                AverageRating = p.Comment != null && p.Comment.Any() 
                    ? p.Comment.Average(c => c.Rating) 
                    : 0
            })
            .Take(count)
            .ToListAsync();
        
        return topRatedProducts.Select(p => p.Product).ToList();
    }
}