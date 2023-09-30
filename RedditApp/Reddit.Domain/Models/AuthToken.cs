using System;
using System.Text.Json.Serialization;

namespace Reddit.Domain.Models
{
    public class AuthToken
    {
        [JsonPropertyName("access_token")]
        public string? Token { get; set; }
        [JsonPropertyName("token_type")]
        public string? TokenType { get; set; }
        [JsonPropertyName("expire_in")]
        public int Expire { get; set; }
        [JsonPropertyName("scope")]
        public string? Scope { get; set; }
        [JsonPropertyName("error")]
        public string? Error { get; set; }
    }
}
