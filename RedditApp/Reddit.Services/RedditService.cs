using Reddit.Domain.Data;
using Reddit.Domain.Models;
using Reddit.Domain.Services.Abstractions;
using Reddit.Controllers;

namespace Reddit.Services
{
    public class RedditService : IRedditService
    {
        private readonly IRedditApi _redditApi;

        public RedditService(IRedditApi redditApi)
        {
            _redditApi = redditApi;
        }

        #region Public Methods
        public async Task<string> RetrieveToken(string code, string redirectUri, string userId, string secret, string? url)
        {
            return await _redditApi.RetrieveToken(code, redirectUri, userId, secret, url);
        }

        public async Task<RedditPostModel> GetNewPosts(string accessToken, string subReddit, int limit)
        {
            IEnumerable<RedditPost>? selectedPosts = null;
            IEnumerable<RedditPost>? topPosts = null;
            IEnumerable<TopUser>? topUsers = null;

            var posts = await _redditApi.GetNewPosts(accessToken, subReddit, limit);
            if (posts.Any())
            {
                selectedPosts = GetPosts(posts); 

                var votes = posts.Where(a => a.UpVotes != 0).Select(s => s.UpVotes).Distinct().OrderByDescending(a => a).ToList();
                if (votes.Count > 5)
                    votes = votes.Take(5).ToList();//take first 5 top votes. 

                //Get top Posts
                topPosts = GetPosts(posts.Where(s => votes.Contains(s.UpVotes))); 

                //Get top users - groupby users and take top counts.
                topUsers = posts.GroupBy(a => a.Author).Select(g => new TopUser() { User = g.Key, Count = g.Count() }).Where(a => a.Count > 1).OrderByDescending(a => a.Count).ToList();
                if (topUsers.Count() > 5)
                    topUsers = topUsers.Take(5).ToList();
            }
            return new RedditPostModel()
            {
                Posts = selectedPosts,
                TopPosts = topPosts,
                TopUsers = topUsers
            };
        }

        #endregion

        #region Private Methods
        private IEnumerable<RedditPost> GetPosts(IEnumerable<Post> posts)
        {
            return posts.Select(a => new RedditPost()
            {
                Title = a.Title,
                Content = a.Listing.SelfText,
                SubReddit = a.Subreddit,
                PostedDate = a.Created,
                ImageURL = a.Listing.URL
            }).OrderByDescending(s => s.PostedDate);
        }
        #endregion
    }
}
