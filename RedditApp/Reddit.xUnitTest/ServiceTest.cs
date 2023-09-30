using NSubstitute;
using Reddit.Data;
using Reddit.Domain.Data;
using Reddit.Domain.Services.Abstractions;
using Reddit.Services;

namespace Reddit.xUnitTest
{
    public class ServiceTest
    {
        private readonly IRedditApi _redditApi = Substitute.For<IRedditApi>();
        private readonly IRedditService _redditService = Substitute.For<IRedditService>();
        
        [Fact]
        public async Task ReturnsPost()
        {
            RedditApi api = new();
            RedditService service = new(api);
            string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjk2MTc4NzExLjUwODUxNywiaWF0IjoxNjk2MDkyMzExLjUwODUxNywianRpIjoiNUlyS19WYXJmbUx6c0Y3ZmtKeC1VSFJBSEY4aGtRIiwiY2lkIjoiQXlfb0dGVnNSeDJ4ZnFoSmFKWVp2ZyIsImxpZCI6InQyX2ttbnpmdDhwcyIsImFpZCI6InQyX2ttbnpmdDhwcyIsImxjYSI6MTY5NTg2OTU1NjEwOCwic2NwIjoiZUp5S1ZpcEtUVXhSaWdVRUFBRF9fd3ZFQXBrIiwiZmxvIjo4fQ.snKWr4A5bCAoTDTA_9kVjHUETmKf2gEL8bdveqevPfycf0j60RHMUtPEA40ijMSDQEc8UktSU3XNEPi2GJ5gh2Xc2atlFhLZtAHJ6fuLeYhhj2fv-72vuNsRFKqPlCJNwb3D4JXlZc-k3KeAwpvpkIiNZkOYpGaARUlfo59jLTzEQMxYNHsFR3x525SzfHeKD1u3Jv1uZRXwuy98NGYYG00JM5jOKuK76Kwmjpvfr1VR7DkQcnxowQhKjm2Qq2yz5eZT3vwL4sL3dlmWH_UbID0SDaDyoMZWdcwBTV3txQkZi9NZEHJtndZvLBxtaL6m6MB2KN1tYvxPZmGp-_IGJw";
            var result = await service.GetNewPosts(token, "wallstreetbets", 100);

            Assert.NotNull(result);
            Assert.NotNull(result.Posts);
            Assert.Equal(100, result.Posts.Count());
        }

        [Fact]
        public async Task InvalidSureddit()
        {
            try
            {
                RedditApi api = new();
                RedditService service = new(api);
                string token = "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNjk2MTc4NzExLjUwODUxNywiaWF0IjoxNjk2MDkyMzExLjUwODUxNywianRpIjoiNUlyS19WYXJmbUx6c0Y3ZmtKeC1VSFJBSEY4aGtRIiwiY2lkIjoiQXlfb0dGVnNSeDJ4ZnFoSmFKWVp2ZyIsImxpZCI6InQyX2ttbnpmdDhwcyIsImFpZCI6InQyX2ttbnpmdDhwcyIsImxjYSI6MTY5NTg2OTU1NjEwOCwic2NwIjoiZUp5S1ZpcEtUVXhSaWdVRUFBRF9fd3ZFQXBrIiwiZmxvIjo4fQ.snKWr4A5bCAoTDTA_9kVjHUETmKf2gEL8bdveqevPfycf0j60RHMUtPEA40ijMSDQEc8UktSU3XNEPi2GJ5gh2Xc2atlFhLZtAHJ6fuLeYhhj2fv-72vuNsRFKqPlCJNwb3D4JXlZc-k3KeAwpvpkIiNZkOYpGaARUlfo59jLTzEQMxYNHsFR3x525SzfHeKD1u3Jv1uZRXwuy98NGYYG00JM5jOKuK76Kwmjpvfr1VR7DkQcnxowQhKjm2Qq2yz5eZT3vwL4sL3dlmWH_UbID0SDaDyoMZWdcwBTV3txQkZi9NZEHJtndZvLBxtaL6m6MB2KN1tYvxPZmGp-_IGJw";
                var result = await service.GetNewPosts(token, "fun", 100);
            }
            catch(Exception ex)
            {
                Assert.Equal("Reddit API returned Forbidden (403) response.", ex.Message); 
            }
        }

        [Fact]
        public async Task InvalidToken()
        {
            try
            {
                RedditApi api = new();
                RedditService service = new(api);
                string token = "eyJhbGciOZCI6IlNIQTI1wijMSDQEc8UktSU3XNEPi2x525Szf";
                var result = await service.GetNewPosts(token, "wallstreetbets", 100);
            }
            catch (Exception ex)
            {
                Assert.Equal("Reddit API returned Unauthorized (401) response.", ex.Message);
            }
        }
    }
}
