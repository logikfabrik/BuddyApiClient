namespace BuddyApiClient.Core.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record Error
    {
        [JsonPropertyName("message")]
        public string? Message { get; set; }
    }
}