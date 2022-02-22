namespace BuddyApiClient.CurrentUser.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdateUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }
    }
}