using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Category> CreateCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> GetCategoryNameAsync(string categoryName)
    {
        return await _context.Categories.FirstOrDefaultAsync(b => b.CategoryName == categoryName);
    }
}