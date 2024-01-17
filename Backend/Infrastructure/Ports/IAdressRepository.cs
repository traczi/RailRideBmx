using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface IAdressRepository
{
    Task AddAddressAsync(Address address);
}