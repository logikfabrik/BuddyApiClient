namespace BuddyApiClient.PermissionSets.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.PermissionSets.Models.Response;

    public sealed record UpdatePermissionSet
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("repository_access_level")]
        public RepositoryAccessLevel? RepositoryAccessLevel { get; set; }

        [JsonPropertyName("pipeline_access_level")]
        public PipelineAccessLevel? PipelineAccessLevel { get; set; }

        [JsonPropertyName("sandbox_access_level")]
        public SandboxAccessLevel? SandboxAccessLevel { get; set; }
    }
}