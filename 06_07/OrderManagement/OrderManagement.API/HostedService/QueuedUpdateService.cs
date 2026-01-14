using OrderManagement.Api.HostedService;
using OrderManagement.Logic;

namespace OrderManagement.API.HostedService;

public class QueuedUpdateService(
    IServiceProvider serviceProvider,
    UpdateChannel updateChannel, 
    ILogger<QueuedUpdateService> logger): BackgroundService {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) {
        using var scope = serviceProvider.CreateScope();
        
        var logic = scope.ServiceProvider.GetRequiredService<IOrderManagementLogic>();

        await foreach (var customerId in updateChannel.ReadAllAsync(stoppingToken)) {
            await logic.UpdateTotalRevenueAsync(customerId);
            logger.LogInformation($"Updated TotalRevenue information for customer {customerId}");
        }

        // while (!stoppingToken.IsCancellationRequested) {
        //     logger.LogInformation("QueuedUpdateService logged");
        //     await Task.Delay(1000, stoppingToken);
        // }
    }
}