using MelbergFramework.Infrastructure.Redis;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Redis.Contexts;

public class DefaultContext : RedisContext
{
    public DefaultContext(IConfiguration provider) : base(provider) { }
}