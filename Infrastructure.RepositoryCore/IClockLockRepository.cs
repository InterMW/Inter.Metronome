namespace Infrastructure.RepositoryCore;
public interface IClockLockRepository
{
    Task<bool> TryLockClockDocumentAsync();
    Task UnlockClockDocumentAsync();
}