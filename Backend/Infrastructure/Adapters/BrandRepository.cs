using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Brand> CreateBrandAsync(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return brand;
    }

    public async Task<Brand> GetBrandNameAsync(string brandName)
    {
        return await _context.Brands.FirstOrDefaultAsync(b => b.BrandName == brandName);
    }
}