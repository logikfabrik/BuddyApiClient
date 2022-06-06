namespace BuddyApiClient.Members.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public record MemberDetails : DocumentResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("id")]
        public MemberId Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("avatar_url")]
        public Uri? AvatarUrl { get; set; }

        [JsonPropertyName("admin")]
        public bool Admin { get; set; }

        [JsonPropertyName("workspace_owner")]
        public bool WorkspaceOwner { get; set; }
    }
}