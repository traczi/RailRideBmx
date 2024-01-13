using Core.Domain.DTOs;
using Core.Domain.Entity;

namespace Application;

public interface IConfigurationBMXService
{
    Task<ConfigurationBMXDto> CreateOrUpdateConfigurationAsync(ConfigurationBMXDto configurationBmx);
    Task ValidationComponent(Guid? productId, string category);
    Task<float> CalculateTotalPriceAsync(Guid configId);
    Task<float> GetProductPrice(Guid? productId);
}