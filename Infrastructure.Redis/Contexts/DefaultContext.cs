using MelbergFramework.Infrastructure.Redis;
using Microsoft.Extensions.Options;

namespace Infrastructure.Redis.Contexts;

public class DefaultContext : RedisContext
{
    public DefaultContext(
            IOptions<RedisConnectionOptions<DefaultContext>> options,
            IConnector connector) : base(options.Value, connector) { } }
