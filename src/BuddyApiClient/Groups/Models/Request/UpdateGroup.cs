namespace BuddyApiClient.Groups.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdateGroup
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}