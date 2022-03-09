namespace BuddyApiClient.Core.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record ErrorResponse
    {
        [JsonPropertyName("errors")]
        public IEnumerable<Error>? Errors { get; set; }
    }
}