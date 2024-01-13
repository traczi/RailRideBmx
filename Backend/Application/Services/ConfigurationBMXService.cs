using Core.Domain.DTOs;
using Core.Domain.Entity;
using Core.Ports;
using Stripe;

namespace Application.Services;

public class ConfigurationBmxService : IConfigurationBMXService
{
    private readonly IConfigurationBMXRepository _configurationBmxRepository;
    private readonly IProductRepository _productRepository;

    public ConfigurationBmxService(IConfigurationBMXRepository configurationBmxRepository, IProductRepository productRepository)
    {
        _configurationBmxRepository = configurationBmxRepository;
        _productRepository = productRepository;
    }

    public async Task<ConfigurationBMXDto> CreateOrUpdateConfigurationAsync(ConfigurationBMXDto configurationBmx)
    {
        if (configurationBmx.FrameId.HasValue) await ValidationComponent(configurationBmx.FrameId.Value, "Frame");
        if (configurationBmx.HandlebarId.HasValue) await ValidationComponent(configurationBmx.HandlebarId.Value, "Handlebar");
        if (configurationBmx.HandlebarCuffId.HasValue) await ValidationComponent(configurationBmx.HandlebarCuffId.Value, "HandlebarCuff");
        if (configurationBmx.HandlebarCapId.HasValue) await ValidationComponent(configurationBmx.HandlebarCapId.Value, "HandlebarCap");
        if (configurationBmx.ForkId.HasValue) await ValidationComponent(configurationBmx.ForkId.Value, "Fork");
        if (configurationBmx.GallowsId.HasValue) await ValidationComponent(configurationBmx.GallowsId.Value, "Gallows");
        if (configurationBmx.HeadsetId.HasValue) await ValidationComponent(configurationBmx.HeadsetId.Value, "Headset");
        if (configurationBmx.RotorId.HasValue) await ValidationComponent(configurationBmx.RotorId.Value, "Rotor");
        if (configurationBmx.SaddleId.HasValue) await ValidationComponent(configurationBmx.SaddleId.Value, "Saddle");
        if (configurationBmx.SaddleStemId.HasValue) await ValidationComponent(configurationBmx.SaddleStemId.Value, "SaddleStem");
        if (configurationBmx.SaddleClampId.HasValue) await ValidationComponent(configurationBmx.SaddleClampId.Value, "SaddleClamp");
        if (configurationBmx.WheelId.HasValue) await ValidationComponent(configurationBmx.WheelId.Value, "Wheel");
        if (configurationBmx.TireId.HasValue) await ValidationComponent(configurationBmx.TireId.Value, "Tire");
        if (configurationBmx.RimId.HasValue) await ValidationComponent(configurationBmx.RimId.Value, "Rim");
        if (configurationBmx.SpokesId.HasValue) await ValidationComponent(configurationBmx.SpokesId.Value, "Spokes");
        if (configurationBmx.HubsId.HasValue) await ValidationComponent(configurationBmx.HubsId.Value, "Hubs");
        if (configurationBmx.ChainsId.HasValue) await ValidationComponent(configurationBmx.ChainsId.Value, "Chains");
        if (configurationBmx.FrontBrakesId.HasValue) await ValidationComponent(configurationBmx.FrontBrakesId.Value, "FrontBrakes");
        if (configurationBmx.RearBrakesId.HasValue) await ValidationComponent(configurationBmx.RearBrakesId.Value, "RearBrakes");
        if (configurationBmx.AssemblyId.HasValue) await ValidationComponent(configurationBmx.AssemblyId.Value, "Assembly");
        if (configurationBmx.PedalId.HasValue) await ValidationComponent(configurationBmx.PedalId.Value, "Pedal");
        if (configurationBmx.PedalArmsId.HasValue) await ValidationComponent(configurationBmx.PedalArmsId.Value, "PedalArms");
        if (configurationBmx.DiskId.HasValue) await ValidationComponent(configurationBmx.DiskId.Value, "Disk");
        if (configurationBmx.CrankSetId.HasValue) await ValidationComponent(configurationBmx.CrankSetId.Value, "CrankSet");
        if (configurationBmx.PegsId.HasValue) await ValidationComponent(configurationBmx.PegsId.Value, "Pegs");
        var config = ConvertDtoToEntity(configurationBmx);
        var configCreate = await _configurationBmxRepository.CreateOrUpdateConfigurationAsync(config);
        return ConvertEntityToDto(configCreate);
    }

    public async Task ValidationComponent(Guid? productId, string category)
    {
        if (!productId.HasValue || productId.Value == Guid.Empty)
        {
            return;
        }
        
        var product = await _productRepository.GetProductByIdAsync(productId.Value);
        if (product == null || product.ConfigCategory != category)
        {
            throw new Exception($"Invalid product for category {category}");
        }
    }

    public async Task<float> CalculateTotalPriceAsync(Guid configId)
    {
        var config = await _configurationBmxRepository.GetConfigByIdAsync(configId);
        if (config == null)
        {
            throw new Exception("Config not found");
        }
        float totalPrice = 0;
        totalPrice += await GetProductPrice(config.FrameId);
        totalPrice += await GetProductPrice(config.HandlebarId);
        totalPrice += await GetProductPrice(config.HandlebarCuffId);
        totalPrice += await GetProductPrice(config.HandlebarCapId);
        totalPrice += await GetProductPrice(config.ForkId);
        totalPrice += await GetProductPrice(config.GallowsId);
        totalPrice += await GetProductPrice(config.HeadsetId);
        totalPrice += await GetProductPrice(config.RotorId);
        totalPrice += await GetProductPrice(config.SaddleId);
        totalPrice += await GetProductPrice(config.SaddleStemId);
        totalPrice += await GetProductPrice(config.SaddleClampId);
        totalPrice += await GetProductPrice(config.WheelId);
        totalPrice += await GetProductPrice(config.TireId);
        totalPrice += await GetProductPrice(config.RimId);
        totalPrice += await GetProductPrice(config.SpokesId);
        totalPrice += await GetProductPrice(config.HubsId);
        totalPrice += await GetProductPrice(config.ChainsId);
        totalPrice += await GetProductPrice(config.FrontBrakesId);
        totalPrice += await GetProductPrice(config.RearBrakesId);
        totalPrice += await GetProductPrice(config.AssemblyId);
        totalPrice += await GetProductPrice(config.PedalId);
        totalPrice += await GetProductPrice(config.PedalArmsId);
        totalPrice += await GetProductPrice(config.DiskId);
        totalPrice += await GetProductPrice(config.CrankSetId);
        totalPrice += await GetProductPrice(config.PegsId);
        return totalPrice;
    }

    public async Task<float> GetProductPrice(Guid? productId)
    {
        if (!productId.HasValue)
        {
            return 0;
        }

        var product = await _productRepository.GetProductByIdAsync(productId.Value);
        return product?.Price ?? 0;
    }

    private ConfigurationBMX ConvertDtoToEntity(ConfigurationBMXDto dto)
    {
        return new ConfigurationBMX
        {
            UserId = dto.UserId,
            NameConfiguration = dto.NameConfiguration,
            FrameId = dto.FrameId,
            HandlebarId = dto.HandlebarId,
            HandlebarCuffId = dto.HandlebarCuffId,
            HandlebarCapId = dto.HandlebarCapId,
            ForkId = dto.ForkId,
            GallowsId = dto.GallowsId,
            HeadsetId = dto.HeadsetId,
            RotorId = dto.RotorId,
            SaddleId = dto.SaddleId,
            SaddleStemId = dto.SaddleStemId,
            SaddleClampId = dto.SaddleClampId,
            WheelId = dto.WheelId,
            TireId = dto.TireId,
            RimId = dto.RimId,
            SpokesId = dto.SpokesId,
            HubsId = dto.HubsId,
            ChainsId = dto.ChainsId,
            FrontBrakesId = dto.FrontBrakesId,
            RearBrakesId = dto.RearBrakesId,
            AssemblyId = dto.AssemblyId,
            PedalId = dto.PedalId,
            PedalArmsId = dto.PedalArmsId,
            DiskId = dto.DiskId,
            CrankSetId = dto.CrankSetId,
            PegsId = dto.PegsId
        };
    }
    
    private ConfigurationBMXDto ConvertEntityToDto(ConfigurationBMX entity)
    {
        return new ConfigurationBMXDto
        {
            NameConfiguration = entity.NameConfiguration,
            UserId = entity.UserId,
            FrameId = entity.FrameId,
            HandlebarId = entity.HandlebarId,
            HandlebarCuffId = entity.HandlebarCuffId,
            HandlebarCapId = entity.HandlebarCapId,
            ForkId = entity.ForkId,
            GallowsId = entity.GallowsId,
            HeadsetId = entity.HeadsetId,
            RotorId = entity.RotorId,
            SaddleId = entity.SaddleId,
            SaddleStemId = entity.SaddleStemId,
            SaddleClampId = entity.SaddleClampId,
            WheelId = entity.WheelId,
            TireId = entity.TireId,
            RimId = entity.RimId,
            SpokesId = entity.SpokesId,
            HubsId = entity.HubsId,
            ChainsId = entity.ChainsId,
            FrontBrakesId = entity.FrontBrakesId,
            RearBrakesId = entity.RearBrakesId,
            AssemblyId = entity.AssemblyId,
            PedalId = entity.PedalId,
            PedalArmsId = entity.PedalArmsId,
            DiskId = entity.DiskId,
            CrankSetId = entity.CrankSetId,
            PegsId = entity.PegsId
        };
    }
}