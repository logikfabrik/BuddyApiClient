namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record WorkspaceDetails : Response
    {
        [JsonPropertyName("id")]
        public WorkspaceId Id { get; set; }

        // TODO: Is this a user ID?
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }

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