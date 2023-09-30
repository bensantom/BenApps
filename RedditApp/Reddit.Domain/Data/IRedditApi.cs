using Reddit.Controllers;

namespace Reddit.Domain.Data
{
    public interface IRedditApi
    {
        Task<string> RetrieveToken(string code, string redirectUri, string userId, string secret, string? url);
        Task<IEnumerable<Post>> GetNewPosts(string accessToken, string subReddit, int limit);
    }
}
