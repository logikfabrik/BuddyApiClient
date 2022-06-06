namespace BuddyApiClient.CurrentUser.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record CurrentUserDetails : DocumentResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public UserId Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri? AvatarUrl { get; set; }

        [JsonPropertyName("workspaces_url")]
        public Uri? WorkspacesUrl { get; set; }
    }
}