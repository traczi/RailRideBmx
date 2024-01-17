using Core.Domain.Entity;

namespace Infrastructure.Ports;

public interface IConfigurationBMXRepository
{
    Task<ConfigurationBMX> CreateOrUpdateConfigurationAsync(ConfigurationBMX configurationBmx);
    Task<ConfigurationBMX> GetConfigByIdAsync(Guid id);
}