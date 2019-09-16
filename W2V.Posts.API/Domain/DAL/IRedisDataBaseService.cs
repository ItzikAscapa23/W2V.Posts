using System;
using StackExchange.Redis;

namespace W2V.Posts.API.Domain.DAL
{
    public interface IRedisDataBaseService
    {
        TimeSpan KeyExpirationTime { get;}
        ConnectionMultiplexer Connection { get;}
        IDatabase RedisCache { get; }
        
    }
}