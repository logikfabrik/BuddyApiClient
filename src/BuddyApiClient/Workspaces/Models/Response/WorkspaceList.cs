namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record WorkspaceList : Response
    {
        [JsonPropertyName("workspaces")]
        public IEnumerable<WorkspaceOutline>? Workspaces { get; set; }
    }
}