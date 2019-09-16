using StackExchange.Redis;

namespace W2V.Posts.API.Domain.Services
{
    interface IRedisHashEntryConvertible
    {
        HashEntry[] ToHashEntryArray();
    }
}
