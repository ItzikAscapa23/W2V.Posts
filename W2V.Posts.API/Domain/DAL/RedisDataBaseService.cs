using System;
using StackExchange.Redis;
using W2V.Posts.API.Configurations;

namespace W2V.Posts.API.Domain.DAL
{
    //Implementation based on http://taswar.zeytinsoft.com/redis-for-net-developer-connecting-with-c/ 
    public class RedisDataBaseService : IRedisDataBaseService
    {
        private readonly Lazy<ConnectionMultiplexer> _lazyConnection;

        public RedisDataBaseService(IDataBaseConfiguration dataBaseConfiguration)
        {
            KeyExpirationTime = dataBaseConfiguration.KeyExpirationTime;

            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = { dataBaseConfiguration.DbConnectionString }
            };

            _lazyConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(configurationOptions));
        }

        public TimeSpan KeyExpirationTime { get; private set; }
        public ConnectionMultiplexer Connection => _lazyConnection.Value;
        public IDatabase RedisCache => Connection.GetDatabase();
    }
}