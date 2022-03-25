namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record ProjectList : PageResponse
    {
        [JsonPropertyName("projects")]
        public IEnumerable<ProjectSummary> Projects { get; set; } = Enumerable.Empty<ProjectSummary>();

        public override int Count => Projects.Count();
    }
}