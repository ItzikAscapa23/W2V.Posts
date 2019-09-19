using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using W2V.Posts.API.Domain.Services;

namespace W2V.Posts.API.Domain.Models
{
    public class Post : IRedisHashEntryConvertible
    {
        public long? Id { get; set; }
        public string Text { get; set; }
        public long UpVotes { get; set; }
        public long DownVotes { get; set; }
        public DateTime CreationTime { get; set; }
        public HashEntry[] ToHashEntryArray()
        {
            string creationTime = JsonConvert.SerializeObject(CreationTime);
            return  new HashEntry[]
            {
                new HashEntry(nameof(Id),Id), 
                new HashEntry(nameof(Text),Text), 
                new HashEntry(nameof(UpVotes),UpVotes), 
                new HashEntry(nameof(DownVotes),DownVotes), 
                new HashEntry(nameof(CreationTime),creationTime)
            };
        }
    }
}