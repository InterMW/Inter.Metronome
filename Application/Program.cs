using Application.Services;
using Common;
using DomainService;
using Infrastructure.Rabbit.Messages;
using Infrastructure.Rabbit.Publishers;
using Infrastructure.Redis.Contexts;
using Infrastructure.Redis.Repositories;
using Infrastructure.RepositoryCore;
using MelbergFramework.Application;
using MelbergFramework.Infrastructure.Rabbit;
using MelbergFramework.Infrastructure.Rabbit.Messages;
using MelbergFramework.Infrastructure.Redis;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Register(builder.Services,builder.Environment.IsDevelopment());
        var app = builder.Build();
        if(app.Environment.IsDevelopment())
        {
            app.Configuration["Rabbit:ClientDeclarations:Connections:0:Password"] = app.Configuration["rabbit_pass"];
        } 

        app.Run();

    }
    private static void Register(IServiceCollection collection, bool isDevelopment)
    {
        collection.RegisterRequired();
        
        collection.AddHostedService<TickService>();

        collection.AddSingleton<IClock,Clock>();
        
        collection.AddTransient<IMetronomeDomainService, MetronomeDomainService>();
        
        RedisModule.LoadRedisRepository<IClockLockRepository, ClockLockRepository, DefaultContext>(collection);
        
        collection.AddTransient<ITickPublisher, TickPublisher>();
        collection.AddTransient<IMinuteTickPublisher,MinuteTickPublisher>();

        RabbitModule.RegisterPublisher<MinuteMessage>(collection);
        RabbitModule.RegisterPublisher<TickMessage>(collection);
    }
}