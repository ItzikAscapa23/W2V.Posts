using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using W2V.Posts.API.Domain.DAL;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Serialization;

namespace W2V.Posts.API.Domain.Repositories
{
    public class PostsRedisRepository : IPostsRepository
    {
        private readonly IRedisDataBaseService _redisDataBaseService;
        private readonly ISerializer _serializer;

        public PostsRedisRepository(IRedisDataBaseService redisDataBaseService, ISerializer serializer)
        {
            _redisDataBaseService = redisDataBaseService;
            _serializer = serializer;
        }

        public async Task<IEnumerable<Post>> GetTopPosts()
        {
            IEnumerable<Post> posts = null;
            try
            {
                IEnumerable<HashEntry> allKeys = _redisDataBaseService.RedisCache.HashScan("*");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return posts;
        }

        public async Task CreatePost(Post p)
        {
            var tran = _redisDataBaseService.RedisCache.CreateTransaction();
            
            p.Id = await tran.StringIncrementAsync("IdCounter");
            await tran.HashSetAsync(p.Id.ToString(), p.ToHashEntryArray());
            tran.Execute();
            await _redisDataBaseService.RedisCache.HashSetAsync(p.Id.ToString(), p.ToHashEntryArray());
            //_redisDataBaseService.RedisCache.Database.
        }

        public Task IncrementUpVotes(long postId)
        {
            throw new NotImplementedException();
        }

        public Task IncrementDownVotes(long postId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(long postId)
        {
            throw new NotImplementedException();
        }

        public Task<Post> GetPostById(long postId)
        {
            throw new NotImplementedException();
        }

        private async Task<Post> GetPostByKey(string postKey)
        {
            Post recivedPost = null;

            try
            {
                if (_redisDataBaseService.RedisCache.KeyExists(postKey))
                {
                    //var x = await _redisDataBaseService.RedisCache.HashGetAsync(postKey, null, CommandFlags.None);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return recivedPost;
        }

        private Post GetDeserializePost(string postStr)
        {
            return _serializer.Deserialize<Post>(postStr);
        }
    }
}