using Infrastructure.RepositoryCore;
using MelbergFramework.Core.HealthCheck;

namespace DomainService;

public class HeartbeatHealthCheck(IClockLockRepository clockRepo) : HealthCheck
{
    private long LastHit = 0;
    public override string Name => "HeartBeatHealthCheck";

    public void MarkTime(long now) => LastHit = now;

    public override async Task<bool> IsOk(CancellationToken token) =>
        DateTime.UnixEpoch.AddSeconds(await clockRepo.GetAction())> DateTime.UtcNow.AddSeconds(-3);
}
