using Infrastructure.RepositoryCore;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Publishers;

namespace Infrastructure.Rabbit.Publishers;

public class TickPublisher : ITickPublisher
{
    private readonly IStandardPublisher<TickMessage> _publisher;
    public TickPublisher(IStandardPublisher<TickMessage> publisher)
    {
        _publisher = publisher;
    }

    public void SendSecondTick() => _publisher.Send(new TickMessage()); 
}