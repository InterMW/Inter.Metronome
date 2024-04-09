using Application;
using Application.Services;
using Infrastructure.Rabbit.Messages;
using Infrastructure.Redis.Contexts;
using LightBDD.MsTest3;
using MelbergFramework.Application;
using MelbergFramework.ComponentTesting.Rabbit;
using MelbergFramework.ComponentTesting.Redis;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests.Features;

public class BaseTestFrame : FeatureFixture
{
    public BaseTestFrame()
    {
        App = MelbergHost.CreateHost<AppRegistrator>()
            .AddServices(_ => 
            {
                _.OverridePublisher<TickMessage>();
                _.OverridePublisher<MinuteMessage>();
                _.OverrideRedisContext<DefaultContext>();
            })
            .AddControllers()
            .Build();
    }
    public WebApplication App;

    public T GetClass<T>() => (T)App
        .Services
        .GetRequiredService(typeof(T));
    
    public TickService GetTickService() =>
        (TickService)App
            .Services
            .GetServices<IHostedService>()
            .Where(_ => 
                    _.GetType() == typeof(TickService))
            .First();

}
