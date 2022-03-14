namespace BuddyApiClient.Projects.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdateProject
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }
    }
}