namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record WorkspaceList : CollectionResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("workspaces")]
        public IEnumerable<WorkspaceSummary> Workspaces { get; set; } = Enumerable.Empty<WorkspaceSummary>();
    }
}