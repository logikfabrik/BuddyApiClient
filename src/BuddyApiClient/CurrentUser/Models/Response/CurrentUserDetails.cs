namespace BuddyApiClient.CurrentUser.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record CurrentUserDetails : ApiObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonPropertyName("workspaces_url")]
        public Uri WorkspacesUrl { get; set; }
    }
}