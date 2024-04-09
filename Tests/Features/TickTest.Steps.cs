using MelbergFramework.ComponentTesting.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Rabbit.Publishers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Features;

public partial class TickTest
{
    private async Task Begin()
    {
        await App.StartAsync();
    }
    private async Task Wait()
    {
        await Task.Delay(3000);
    }

    private async Task Stop()
    {
        await App.StopAsync();
    }
    
    private async Task Three_ticks_were_sent()
    {
        var publisher = (MockPublisher<TickMessage>)GetClass<IStandardPublisher<TickMessage>>();
        Assert.AreEqual(3, publisher.SentMessages.Count);
    }
}
