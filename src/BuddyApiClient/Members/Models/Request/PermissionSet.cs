namespace BuddyApiClient.Members.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record PermissionSet
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}