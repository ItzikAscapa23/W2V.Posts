using System;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using W2V.Posts.API.Configurations;

namespace W2V.Posts.API.Domain.DAL
{
    //Implementation based on http://taswar.zeytinsoft.com/redis-for-net-developer-connecting-with-c/ 
    public class RedisDataBaseService : IRedisDataBaseService
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisDataBaseService(IOptions<RedisDataBaseConfiguration> dataBaseConfiguration)
        {
            KeyExpirationTime = dataBaseConfiguration.Value.KeyExpirationTime;

            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { dataBaseConfiguration.Value.DbConnectionString }
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public TimeSpan KeyExpirationTime { get; private set; }
        public ConnectionMultiplexer Connection => _lazyConnection.Value;
        public IDatabase RedisCache => Connection.GetDatabase();
    }
}