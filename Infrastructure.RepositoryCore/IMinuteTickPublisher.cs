namespace Infrastructure.RepositoryCore;

public interface IMinuteTickPublisher
{
    void SendMinuteTick();
}