using Core.DTOs;

namespace Application.IServices;

public interface IConfigurationBMXService
{
    Task<ConfigurationBmxDto> CreateOrUpdateConfigurationAsync(ConfigurationBmxDto configurationBmx);
    Task ValidationComponent(Guid? productId, string category);
    Task<float> CalculateTotalPriceAsync(Guid configId);
    Task<float> GetProductPrice(Guid? productId);
}