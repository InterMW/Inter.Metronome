using Infrastructure.Redis.Contexts;
using Infrastructure.RepositoryCore;
using MelbergFramework.Infrastructure.Redis;
namespace Infrastructure.Redis.Repositories;

public class ClockLockRepository : RedisRepository<DefaultContext>, IClockLockRepository
{
    public ClockLockRepository(DefaultContext context) : base(context)
    {
    }

    public Task<bool> TryLockClockDocumentAsync() =>
        DB.LockTakeAsync(Key, 1, TimeSpan.FromMilliseconds(600));
    
    public Task UnlockClockDocumentAsync() =>
        DB.LockReleaseAsync(Key,1);
    
    static string Key = "clock_lock_doc";
}
