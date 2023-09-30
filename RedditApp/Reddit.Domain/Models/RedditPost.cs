using System.Text.Json.Serialization;

namespace Reddit.Domain.Models
{
    public class RedditPost
    {
        [JsonPropertyName("Title")]
        public string Title { get; set; } = string.Empty;
        [JsonPropertyName("URL")]
        public string URL { get; set; } = string.Empty;
        [JsonPropertyName("ImageURL")]
        public string ImageURL { get; set; } = string.Empty;
        [JsonPropertyName("Content")]
        public string Content { get; set; } = string.Empty;
        [JsonPropertyName("SubReddit")]
        public string SubReddit { get; set; } = string.Empty;
        [JsonPropertyName("PostedDate")]
        public DateTime PostedDate { get; set; } = DateTime.Now;
    }
}
