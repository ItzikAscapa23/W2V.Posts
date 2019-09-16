using System;
using Newtonsoft.Json;
using StackExchange.Redis;
using W2V.Posts.API.Domain.Services;

namespace W2V.Posts.API.Domain.Models
{
    public class Post : IRedisHashEntryConvertible
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime CreationTime { get; set; }
        //public int Score { get; set; }
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