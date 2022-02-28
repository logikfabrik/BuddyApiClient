namespace BuddyApiClient.PermissionSets.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.PermissionSets.Models.Response;
    using EnsureThat;

    public sealed record CreatePermissionSet
    {
        public CreatePermissionSet(string name)
        {
            Name = Ensure.String.IsNotNullOrWhiteSpace(name, nameof(name));
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("repository_access_level")]
        public RepositoryAccessLevel RepositoryAccessLevel { get; set; }

        [JsonPropertyName("pipeline_access_level")]
        public PipelineAccessLevel PipelineAccessLevel { get; set; }

        [JsonPropertyName("sandbox_access_level")]
        public SandboxAccessLevel SandboxAccessLevel { get; set; }
    }
}