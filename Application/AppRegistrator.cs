using Application.Services;
using DomainService;
using Infrastructure.Rabbit.Messages;
using Infrastructure.Rabbit.Publishers;
using Infrastructure.Redis.Contexts;
using Infrastructure.Redis.Repositories;
using Infrastructure.RepositoryCore;
using MelbergFramework.Application;
using MelbergFramework.Core.Time;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Redis;

namespace Application;

public class AppRegistrator : Registrator
{
    public AppRegistrator() { }

    public override void RegisterServices(IServiceCollection services)
    {
        services.AddHostedService<TickService>();

        services.AddSingleton<IClock,Clock>();
        
        services.AddTransient<IMetronomeDomainService, MetronomeDomainService>();
        
        RedisDependencyModule.LoadRedisRepository<
            IClockLockRepository,
            ClockLockRepository,
            DefaultContext>(services);
        
        services.AddTransient<ITickPublisher, TickPublisher>();
        services.AddTransient<IMinuteTickPublisher,MinuteTickPublisher>();

        RabbitModule.RegisterPublisher<MinuteMessage>(services);
        RabbitModule.RegisterPublisher<TickMessage>(services);
    }
}
