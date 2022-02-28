namespace BuddyApiClient.Members.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdateMember
    {
        [JsonPropertyName("admin")]
        public bool Admin { get; set; }
    }
}