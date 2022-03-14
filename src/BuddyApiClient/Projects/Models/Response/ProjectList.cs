namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record ProjectList : PageResponse
    {
        [JsonPropertyName("projects")]
        public IEnumerable<ProjectSummary>? Projects { get; set; }

        public override int Count => Projects?.Count() ?? 0;
    }
}