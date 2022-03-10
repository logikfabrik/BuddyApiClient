namespace BuddyApiClient.PermissionSets.Models.Request
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.PermissionSets.Models.Response;
    using EnsureThat;

    public sealed record CreatePermissionSet
    {
        public CreatePermissionSet(string name)
        {
            Name = Ensure.String.IsNotNullOrEmpty(name, nameof(name));
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonIgnore]
        public RepositoryAccessLevel RepositoryAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="RepositoryAccessLevel" />.
        /// </summary>
        [JsonPropertyName("repository_access_level")]
        public string RepositoryAccessLevelJson => RepositoryAccessLevelJsonConverter.ConvertTo(RepositoryAccessLevel);

        [JsonIgnore]
        public PipelineAccessLevel PipelineAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="PipelineAccessLevel" />.
        /// </summary>
        [JsonPropertyName("pipeline_access_level")]
        public string PipelineAccessLevelJson => PipelineAccessLevelJsonConverter.ConvertTo(PipelineAccessLevel);

        [JsonIgnore]
        public SandboxAccessLevel SandboxAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="SandboxAccessLevel" />.
        /// </summary>
        [JsonPropertyName("sandbox_access_level")]
        public string SandboxAccessLevelJson => SandboxAccessLevelJsonConverter.ConvertTo(SandboxAccessLevel);
    }
}