using System.Security.Claims;
using Application;
using Application.IServices;
using Core.Domain.Entity;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace RailRideBMXHexagonale.Controllers;

public class ConfigurationBMXController : ApiController
{
    private readonly IConfigurationBMXService _configurationBmxService;

    public ConfigurationBMXController(IConfigurationBMXService configurationBmxService)
    {
        _configurationBmxService = configurationBmxService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateConfiguration([FromBody]ConfigurationBmxDto configurationBmxDto)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _configurationBmxService.CreateOrUpdateConfigurationAsync(configurationBmxDto);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    [HttpGet("totalprice/{configurationId}")]
    public async Task<IActionResult> GetTotalPrice(Guid configurationId)
    {
        try
        {
            var totalPrice = await _configurationBmxService.CalculateTotalPriceAsync(configurationId);
            return Ok(new { TotalPrice = totalPrice });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}