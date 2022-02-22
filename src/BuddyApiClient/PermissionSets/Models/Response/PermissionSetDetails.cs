namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models;

    public sealed record PermissionSetDetails : ApiObject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }

        [JsonPropertyName("repository_access_level")]
        public RepositoryAccessLevel RepositoryAccessLevel { get; set; }

        [JsonPropertyName("pipeline_access_level")]
        public PipelineAccessLevel PipelineAccessLevel { get; set; }

        [JsonPropertyName("sandbox_access_level")]
        public SandboxAccessLevel SandboxAccessLevel { get; set; }
    }
}