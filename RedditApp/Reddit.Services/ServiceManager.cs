using Reddit.Domain.Services.Abstractions;

namespace Reddit.Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly IRedditService _redditService;

        public ServiceManager(IRedditService redditService)
        {
            _redditService = redditService;
        }

        public IRedditService RedditService => _redditService;
    }
}