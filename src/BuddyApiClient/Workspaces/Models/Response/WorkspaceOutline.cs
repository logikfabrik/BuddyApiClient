namespace BuddyApiClient.Workspaces.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record WorkspaceOutline
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("domain")]
        public string? Domain { get; set; }
    }
}