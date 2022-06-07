namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;
    using BuddyApiClient.Members.Models;

    public sealed record WorkspaceDetails : DocumentResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public WorkspaceId Id { get; set; }

        [JsonPropertyName("owner_id")]
        public MemberId OwnerId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("domain")]
        public Domain Domain { get; set; }

        [JsonPropertyName("frozen")]
        public bool Frozen { get; set; }

        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }
    }
}