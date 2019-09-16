using System;

namespace W2V.Posts.API.Configurations
{
    public interface IDataBaseConfiguration
    {
        string DbConnectionString { get; set; }
        TimeSpan KeyExpirationTime { get; set; }
    }
}