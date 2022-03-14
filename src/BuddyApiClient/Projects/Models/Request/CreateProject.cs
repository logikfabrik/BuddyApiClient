namespace BuddyApiClient.Projects.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record CreateProject
    {
        public CreateProject(string displayName)
        {
            DisplayName = Ensure.String.IsNotNullOrEmpty(displayName, nameof(displayName));
        }

        [JsonPropertyName("display_name")]
        public string DisplayName { get; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("integration")]
        public Integration? Integration { get; set; }

        [JsonPropertyName("external_project_id")]
        public string? ExternalProjectId { get; set; }

        [JsonPropertyName("git_lab_project_id")]
        public string? GitLabProjectId { get; set; }

        [JsonPropertyName("custom_repo_url")]
        public string? CustomRepositoryUrl { get; set; }

        [JsonPropertyName("custom_repo_user")]
        public string? CustomRepositoryUsername { get; set; }

        [JsonPropertyName("custom_repo_pass")]
        public string? CustomRepositoryPassword { get; set; }
    }
}