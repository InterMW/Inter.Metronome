using Infrastructure.Rabbit.Messages;
using Infrastructure.RepositoryCore;
using MelbergFramework.Infrastructure.Rabbit.Publishers;

namespace Infrastructure.Rabbit.Publishers;

public class MinuteTickPublisher : IMinuteTickPublisher
{
    private readonly IStandardPublisher<MinuteMessage> _publisher;
    public MinuteTickPublisher(IStandardPublisher<MinuteMessage> publisher)
    {
        _publisher = publisher;
    }

    public void SendMinuteTick()
    {
        _publisher.Send(new MinuteMessage());
    }
}