namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;
    using BuddyApiClient.Members.Models.Response;

    public sealed record ProjectDetails : ApiObject
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("created_by")]
        public MemberOutline CreatedBy { get; set; }

        [JsonPropertyName("http_repository")]
        public string? HttpRepository { get; set; }

        [JsonPropertyName("ssh_repository")]
        public string? SshRepository { get; set; }

        [JsonPropertyName("default_branch")]
        public string? DefaultBranch { get; set; }
    }
}