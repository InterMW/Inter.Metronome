using Infrastructure.Redis.Contexts;
using Infrastructure.RepositoryCore;
using MelbergFramework.Infrastructure.Redis;
namespace Infrastructure.Redis.Repositories;

public class ClockLockRepository : RedisRepository<DefaultContext>, IClockLockRepository
{
    public ClockLockRepository(DefaultContext context) : base(context) { }

    public Task<bool> TryLockClockDocumentAsync() =>
        DB.LockTakeAsync(Key, 1, TimeSpan.FromMilliseconds(600));
    
    public Task UnlockClockDocumentAsync() =>
        DB.LockReleaseAsync(Key,1);

    public Task ShowAction(long time) => DB.StringSetAsync("last_hit_health", time);

    public async Task<long> GetAction()
    {
        var result = await DB.StringGetAsync("last_hit_health");
    
        return result.HasValue ? long.Parse(result) : 0;
    }
    static string Key = "clock_lock_doc";
}
