namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record ProjectList : CollectionPageResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("projects")]
        public IEnumerable<ProjectSummary> Projects { get; set; } = Enumerable.Empty<ProjectSummary>();

        public override int Count => Projects.Count();
    }
}