using Core.Domain.Entity;

namespace Core.Ports;

public interface IConfigurationBMXRepository
{
    Task<ConfigurationBMX> CreateOrUpdateConfigurationAsync(ConfigurationBMX configurationBmx);
    Task<ConfigurationBMX> GetConfigByIdAsync(Guid id);
}