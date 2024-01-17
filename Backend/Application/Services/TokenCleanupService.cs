using Infrastructure.Ports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Application.Services;

public class TokenCleanupService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public TokenCleanupService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                await userRepository.CleanUpExpiredPasswordResetTokensAsync();
            }

            await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
        }
    }
}
