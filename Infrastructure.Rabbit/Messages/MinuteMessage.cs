using MelbergFramework.Infrastructure.Rabbit.Messages;

namespace Infrastructure.Rabbit.Messages;

public class MinuteMessage : TickMessage
{
    public override string GetRoutingKey() => "tick.minute";
}