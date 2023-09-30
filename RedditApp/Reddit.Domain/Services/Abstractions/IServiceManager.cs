namespace Reddit.Domain.Services.Abstractions
{
    public interface IServiceManager
    {
        IRedditService RedditService { get; }
    }
}
