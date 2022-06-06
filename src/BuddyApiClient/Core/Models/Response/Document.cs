namespace BuddyApiClient.Core.Models.Response
{
    using System.Text.Json.Serialization;

    public abstract record Document
    {
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }
    }
}