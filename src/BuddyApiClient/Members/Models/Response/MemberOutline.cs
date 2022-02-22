namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record MemberOutline : ApiObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri? AvatarUrl { get; set; }
    }
}