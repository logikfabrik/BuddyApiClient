namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record WorkspaceSummary
    {
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }
    }
}