using Core.Domain.Entity;
using Core.Ports;
using Infrastructure.DbContext;

namespace Infrastructure.Adapters;

public class ConfigurationBMXRepository : IConfigurationBMXRepository
{
    private readonly ApplicationDbContext _context;

    public ConfigurationBMXRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ConfigurationBMX> CreateOrUpdateConfigurationAsync(ConfigurationBMX configurationBmx)
    {
        if (configurationBmx.ConfigurationId == Guid.Empty)
        {
            _context.ConfigurationsBMX.Add(configurationBmx);
        }
        else
        {
            _context.ConfigurationsBMX.Update(configurationBmx);
        }

        await _context.SaveChangesAsync();
        return configurationBmx;
    }

    public async  Task<ConfigurationBMX> GetConfigByIdAsync(Guid id)
    {
        return await _context.ConfigurationsBMX.FindAsync(id);
    }
}