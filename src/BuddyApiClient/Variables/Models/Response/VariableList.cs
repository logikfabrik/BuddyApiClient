namespace BuddyApiClient.Variables.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record VariableList : CollectionResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("variables")]
        public IEnumerable<VariableSummary> Variables { get; set; } = Enumerable.Empty<VariableSummary>();
    }
}