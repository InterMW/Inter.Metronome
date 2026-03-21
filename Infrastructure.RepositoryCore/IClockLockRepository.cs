namespace Infrastructure.RepositoryCore;
public interface IClockLockRepository
{
    Task<bool> TryLockClockDocumentAsync();
    Task UnlockClockDocumentAsync();
    Task ShowAction(long time);
    Task<long> GetAction();
}
