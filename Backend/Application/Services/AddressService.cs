using Application.IServices;
using Core.Domain.Entity;
using Core.DTOs;
using Infrastructure.Ports;

namespace Application.Services;

public class AddressService : IAddressService
{
    private readonly IAdressRepository _addressRepository;

    public AddressService(IAdressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }


    public async Task AddAddressToCartAsync(Guid cartId, AddressDto address)
    {
        var addressEtity = new Address()
        {
            AddressId = Guid.NewGuid(),
            Name = address.Name,
            Line1 = address.Line1,
            Line2 = address.Line2,
            City = address.City,
            State = address.State,
            Country = address.Country,
            PostalCode = address.PostalCode,
            CartId = cartId
        };
        
        Console.WriteLine(addressEtity.CartId);
        await _addressRepository.AddAddressAsync(addressEtity);
    }
}