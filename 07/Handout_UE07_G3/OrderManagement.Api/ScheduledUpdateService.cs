using OrderManagement.Logic;

namespace OrderManagement.Api.BackgroundServices;

public class ScheduledUpdateService : BackgroundService
{
  private static readonly TimeSpan UPDATE_INTERVAL = TimeSpan.FromSeconds(5);

  private readonly IOrderManagementLogic logic;
  private readonly ILogger<ScheduledUpdateService> logger;

  public ScheduledUpdateService(IOrderManagementLogic logic, 
                                ILogger<ScheduledUpdateService> logger)
  {
    this.logic = logic;
    this.logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    // Delay before first call to UpdateTotalRevenues, so that client can check change in TotalRevenue.
    await Task.Delay(UPDATE_INTERVAL, stoppingToken);

    while (!stoppingToken.IsCancellationRequested)
    {
      await logic.UpdateTotalRevenues();
      logger.LogInformation($"Updated all total revenues at {DateTimeOffset.Now}.");

      await Task.Delay(UPDATE_INTERVAL, stoppingToken);
    }
  }
}
