namespace Reddit.Domain.Models
{
    public class AppIdentitySettings
    {
        public string ClientId { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string AppId { get; set; } = string.Empty;
    }
}
