using Core.Domain.Entity;
using Infrastructure.DbContext;
using Infrastructure.Ports;

namespace Infrastructure.Adapters;

public class AdressRepository : IAdressRepository
{
    private readonly ApplicationDbContext _context;

    public AdressRepository(ApplicationDbContext dbContext)
    {
        _context = dbContext;
    }


    public async Task AddAddressAsync(Address address)
    {
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
    }
}