using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;
using W2V.Posts.API.Domain.DAL;
using W2V.Posts.API.Domain.Models;

namespace W2V.Posts.API.Domain.Repositories
{
    public class PostsRedisRepository : IPostsRepository
    {
        private readonly IRedisDataBaseService _redisDataBaseService;

        public PostsRedisRepository(IRedisDataBaseService redisDataBaseService)
        {
            _redisDataBaseService = redisDataBaseService;
        }

        public async Task<IEnumerable<Post>> GetTopPosts(int numOfPosts)
        {
            var topPosts = new List<Post>();
            RedisValue[] postsIds = await _redisDataBaseService.RedisCache.SortedSetRangeByRankAsync("Posts:Score", 0, numOfPosts - 1, Order.Descending);

            foreach (var postId in postsIds)
            {
                string postKey = $"Posts:{postId}";
                HashEntry[] postEntries = await _redisDataBaseService.RedisCache.HashGetAllAsync(postKey);

                Post post = GetPost(postEntries);

                topPosts.Add(post);
            }

            return topPosts;
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            IList<Post> allPosts = new List<Post>();
            RedisValue[] postsIds = await _redisDataBaseService.RedisCache.SortedSetRangeByRankAsync("Posts:Score", order:Order.Descending);

            foreach (var postId in postsIds)
            {
                string postKey = $"Posts:{postId}";
                HashEntry[] postEntries = await _redisDataBaseService.RedisCache.HashGetAllAsync(postKey);

                Post post = GetPost(postEntries);

                allPosts.Add(post);
            }

            return allPosts;
        }

        public async Task CreatePost(Post post)
        {
            post.Id = await _redisDataBaseService.RedisCache.StringIncrementAsync("IdCounter");
            await _redisDataBaseService.RedisCache.HashSetAsync($"Posts:{post.Id}", post.ToHashEntryArray());
            double postScore = CalculatePostScore(post.UpVotes, post.CreationTime);
            await _redisDataBaseService.RedisCache.SortedSetAddAsync("Posts:Score", $"{post.Id}", postScore);
        }

        public async Task IncrementUpVotes(long postId)
        {
            long upVotes = await _redisDataBaseService.RedisCache.HashIncrementAsync($"Posts:{postId}", "UpVotes", 1);
            string creationTimeValue = await _redisDataBaseService.RedisCache.HashGetAsync($"Posts:{postId}", "CreationTime");
            DateTime postCreationTime = ParseCreationTime(creationTimeValue);
            double postScore = CalculatePostScore(upVotes, postCreationTime);
            await _redisDataBaseService.RedisCache.SortedSetAddAsync("Posts:Score", $"{postId}", postScore);
        }

        public async Task IncrementDownVotes(long postId)
        {
            await _redisDataBaseService.RedisCache.HashIncrementAsync($"Posts:{postId}", "DownVotes", 1);
        }

        public async Task DeletePost(long postId)
        {
            await _redisDataBaseService.RedisCache.KeyDeleteAsync($"Posts:{postId}");
            await _redisDataBaseService.RedisCache.SortedSetRemoveAsync("Posts:Score", $"{postId}");
        }

        public async Task UpdatePostText(long postId, string text)
        {
            await _redisDataBaseService.RedisCache.HashSetAsync($"Posts:{postId}", "Text", text);
        }

        private Post GetPost(HashEntry[] postEntries)
        {
            var post = new Post();

            post.Id = (long?)postEntries[0].Value;
            post.Text = postEntries[1].Value;
            post.UpVotes = (long)postEntries[2].Value;
            post.DownVotes = (long)postEntries[3].Value;
            post.CreationTime = ParseCreationTime(postEntries[4].Value.ToString());

            return post;
        }

        private DateTime ParseCreationTime(string postCreationTimeStr)
        {
            return DateTimeOffset.Parse(postCreationTimeStr.Replace("\"", string.Empty)).DateTime;
        }

        private double CalculatePostScore(long upVotes, DateTime creationTime)
        {
            double numberOfSecondsElapsedBetweenCreationToNow = (DateTime.UtcNow - creationTime).TotalSeconds;
            double postScore = 45000 * Math.Log(upVotes, 10) + numberOfSecondsElapsedBetweenCreationToNow;

            return postScore;
        }
    }
}