namespace Reddit.Domain.Models
{
    public class RedditPostModel
    {
        public IEnumerable<RedditPost>? Posts { get; set; } = null;
        public IEnumerable<RedditPost>? TopPosts { get; set; } = null;
        public IEnumerable<TopUser>? TopUsers { get; set; } = null;
    }
}
