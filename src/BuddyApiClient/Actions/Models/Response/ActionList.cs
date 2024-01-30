namespace BuddyApiClient.Actions.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record ActionList : CollectionResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("actions")]
        public IEnumerable<ActionSummary> Actions { get; set; } = Enumerable.Empty<ActionSummary>();
    }
}