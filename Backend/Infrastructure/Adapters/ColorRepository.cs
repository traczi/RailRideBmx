using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Ports;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Adapters;

public class ColorRepository : IColorRepository
{
    private readonly ApplicationDbContext _context;

    public ColorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Color> CreateColorAsync(Color color)
    {
        _context.Colors.Add(color);
        await _context.SaveChangesAsync();
        return color;
    }

    public async Task<Color> GetColorNameAsync(string colorName)
    {
        return await _context.Colors.FirstOrDefaultAsync(c => c.ColorName == colorName);
    }
}