using Core.Domain.Entity;
using Core.DTOs;

namespace Application.IServices;

public interface IAddressService
{
    public Task AddAddressToCartAsync(Guid cartId, AddressDto address);
}