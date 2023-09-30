using Reddit.Domain.Models;

namespace Reddit.Domain.Services.Abstractions
{
    public interface IRedditService
    {
        Task<string> RetrieveToken(string code, string redirectUri, string userId, string secret, string? url);
        Task<RedditPostModel> GetNewPosts(string accessToken, string subReddit, int limit);
    }
}
