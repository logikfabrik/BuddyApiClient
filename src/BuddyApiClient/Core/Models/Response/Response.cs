namespace BuddyApiClient.Core.Models.Response
{
    using System.Text.Json.Serialization;

    public abstract record Response
    {
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }
    }
}