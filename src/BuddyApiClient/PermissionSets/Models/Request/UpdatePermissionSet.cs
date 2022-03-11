namespace BuddyApiClient.PermissionSets.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdatePermissionSet
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonIgnore]
        public RepositoryAccessLevel? RepositoryAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="RepositoryAccessLevel" />.
        /// </summary>
        [JsonPropertyName("repository_access_level")]
        public string? RepositoryAccessLevelJson => RepositoryAccessLevel.HasValue ? RepositoryAccessLevelJsonConverter.ConvertTo(RepositoryAccessLevel.Value) : null;

        [JsonIgnore]
        public PipelineAccessLevel? PipelineAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="PipelineAccessLevel" />.
        /// </summary>
        [JsonPropertyName("pipeline_access_level")]
        public string? PipelineAccessLevelJson => PipelineAccessLevel.HasValue ? PipelineAccessLevelJsonConverter.ConvertTo(PipelineAccessLevel.Value) : null;

        [JsonIgnore]
        public SandboxAccessLevel? SandboxAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="SandboxAccessLevel" />.
        /// </summary>
        [JsonPropertyName("sandbox_access_level")]
        public string? SandboxAccessLevelJson => SandboxAccessLevel.HasValue ? SandboxAccessLevelJsonConverter.ConvertTo(SandboxAccessLevel.Value) : null;
    }
}