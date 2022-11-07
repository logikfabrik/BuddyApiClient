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
        public string? RepositoryAccessLevelJson => RepositoryAccessLevel is null ? null : RepositoryAccessLevelJsonConverter.ConvertTo(RepositoryAccessLevel.Value);

        [JsonIgnore]
        public PipelineAccessLevel? PipelineAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="PipelineAccessLevel" />.
        /// </summary>
        [JsonPropertyName("pipeline_access_level")]
        public string? PipelineAccessLevelJson => PipelineAccessLevel is null ? null : PipelineAccessLevelJsonConverter.ConvertTo(PipelineAccessLevel.Value);

        [JsonIgnore]
        public SandboxAccessLevel? SandboxAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="SandboxAccessLevel" />.
        /// </summary>
        [JsonPropertyName("sandbox_access_level")]
        public string? SandboxAccessLevelJson => SandboxAccessLevel is null ? null : SandboxAccessLevelJsonConverter.ConvertTo(SandboxAccessLevel.Value);
    }
}