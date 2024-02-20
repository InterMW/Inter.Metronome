using DomainService;

namespace Application.Services;

public class TickService : BackgroundService
{
    private readonly IMetronomeDomainService _service;
    public TickService(IMetronomeDomainService service) 
    {
        _service = service;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken) =>
        _service.StartTickGenerator(stoppingToken);
}