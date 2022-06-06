namespace BuddyApiClient.Projects.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;
    using BuddyApiClient.Members.Models.Response;

    public sealed record ProjectDetails : DocumentResponse
    {
        [JsonPropertyName("html_url")]
        public Uri? HtmlUrl { get; set; }

        [JsonPropertyName("name")]
        public ProjectName Name { get; set; }

        [JsonPropertyName("display_name")]
        public string? DisplayName { get; set; }

        [JsonIgnore]
        public Status Status { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Status" />.
        /// </summary>
        [JsonPropertyName("status")]
        public string StatusJson
        {
            set => Status = StatusJsonConverter.ConvertFrom(value);
        }

        [JsonPropertyName("create_date")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("created_by")]
        public MemberSummary? CreatedBy { get; set; }

        [JsonPropertyName("http_repository")]
        public Uri? HttpRepository { get; set; }

        [JsonPropertyName("ssh_repository")]
        public string? SshRepository { get; set; }

        [JsonPropertyName("default_branch")]
        public string? DefaultBranch { get; set; }
    }
}