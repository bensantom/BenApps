using Reddit.Controllers;
using Reddit.Domain.Data;
using Reddit.Inputs;
using System.Net.Http.Headers;
using System.Text;

namespace Reddit.Data
{
    public sealed class RedditApi : IRedditApi
    {
        public RedditClient? RedditClient { get; set; }

        #region Public Methods
        public async Task<string> RetrieveToken(string code, string redirectUri, string userId, string secret, string? url)
        {
            using var client = new HttpClient();
            
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Basic", Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{userId}:{secret}")));
            client.DefaultRequestHeaders.Add("User-Agent", "RestSharp/106.6.9.0");
            var data = new StringContent($"grant_type=authorization_code&code={code}&redirect_uri={redirectUri}", Encoding.UTF8, "application/x-www-form-urlencoded");

            var response = await client.PostAsync(url, data);
            var result = await response.Content.ReadAsStringAsync();
            
            return result;
        }

        public async Task<IEnumerable<Post>> GetNewPosts(string accessToken, string subReddit, int limit)
        {
            List<Post> posts = new();
            try
            {
                await Task.Run(() =>
                    {
                        RedditClient = new RedditClient(accessToken: accessToken);

                        posts = RedditClient.Subreddit(subReddit).Posts.GetNew(new CategorizedSrListingInput(limit: limit));
                    });
            }
            catch (Exception)
            {
                throw;
            }
            return posts;
        }
        #endregion
    }
}
