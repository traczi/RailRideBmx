using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface IBrandRepository
{
    public Task<Brand> CreateBrandAsync(Brand brand);
    public Task<Brand> GetBrandNameAsync(string brandName);
}