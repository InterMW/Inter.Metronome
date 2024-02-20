using Common;
using Infrastructure.RepositoryCore;
using Microsoft.Extensions.Logging;

namespace DomainService;
public interface IMetronomeDomainService
{
    Task StartTickGenerator(CancellationToken cancellationToken);
}

public class MetronomeDomainService : IMetronomeDomainService
{
    private readonly IClockLockRepository _clockLockRepository;
    private readonly ITickPublisher _tickPublisher;
    private readonly IMinuteTickPublisher _minuteTickPublisher;
    private readonly ILogger<MetronomeDomainService> _logger;
    private readonly IClock _clock;
    public MetronomeDomainService(
        IClockLockRepository clockLockRepository,
        ITickPublisher tickPublisher,
        IMinuteTickPublisher minuteTickPublisher,
        IClock clock,
        ILogger<MetronomeDomainService> logger)
    {
        _clockLockRepository = clockLockRepository;
        _minuteTickPublisher = minuteTickPublisher;
        _tickPublisher = tickPublisher;
        _clock = clock;
        _logger = logger;
    }

    public async Task StartTickGenerator(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            // figure out ahead of time who should get to send the message
            await SleepTillThreeQuarterSecond();

            var locked = await _clockLockRepository.TryLockClockDocumentAsync();
            
            // Then sleep till it is time to send the messages
            await SleepTillNextSecond();
                
            if(locked)
            {
                _tickPublisher.SendSecondTick();
                _logger.LogInformation("Tick sent.");

                if(_clock.UtcNow.Second == 0)
                {
                    _minuteTickPublisher.SendMinuteTick();
                    _logger.LogInformation("Minute sent.");
                }
                await _clockLockRepository.UnlockClockDocumentAsync();
            }
        }
    }

    private Task SleepTillThreeQuarterSecond() => Task.Delay(750 - (_clock.UtcNow.Millisecond % 750));
    private Task SleepTillNextSecond() => Task.Delay(1000 -  _clock.UtcNow.Millisecond);
}
