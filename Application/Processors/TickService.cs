using DomainService;

namespace Application.Services;

public class TickService : BackgroundService
{
    private readonly IServiceProvider _service;
    public TickService(IServiceProvider service) 
    {
        _service = service;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            using var scope = _service.CreateScope();
            await scope.ServiceProvider.GetService<IMetronomeDomainService>().RunTick();
        }
    }
}
