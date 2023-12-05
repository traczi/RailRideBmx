using Core.Domain.Entity;
using Core.Ports;
using Infrastructure.DbContext;
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
        var productByCategorie = _context.Products.Where(x => x.Category == category).ToListAsync();
        return await productByCategorie;
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
}