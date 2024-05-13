using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface IColorRepository
{
    public Task<Color> CreateColorAsync(Color color);
    public Task<Color> GetColorNameAsync(string colorName);
    
}