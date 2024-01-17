using Application.IServices;
using Core.Domain.Entity;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class AddressController : ApiController
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }
    
    [HttpPost("AddAddress")]
    public async Task<IActionResult> AddAddressToCart(string cartId,AddressDto addressModel)
    {
        var cartIdGuid = Guid.Parse(cartId);
        await _addressService.AddAddressToCartAsync(cartIdGuid, addressModel);
        return Ok();
    }
    
}