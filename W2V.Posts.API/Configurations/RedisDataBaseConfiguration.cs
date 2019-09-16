using System;

namespace W2V.Posts.API.Configurations
{
    public class RedisDataBaseConfiguration : IDataBaseConfiguration
    {
        public string DbConnectionString { get; set; }
        public TimeSpan KeyExpirationTime { get; set; }
    }
}
