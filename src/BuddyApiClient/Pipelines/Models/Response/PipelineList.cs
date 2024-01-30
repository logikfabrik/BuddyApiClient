namespace BuddyApiClient.Pipelines.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record PipelineList : CollectionPageResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("pipelines")]
        public IEnumerable<PipelineSummary> Pipelines { get; set; } = Enumerable.Empty<PipelineSummary>();

        public override int Count => Pipelines.Count();
    }
}
