namespace BuddyApiClient.PermissionSets.Models.Response
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    public sealed record PermissionSetSummary : Response
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonIgnore]
        public Type Type { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("type")]
        public string TypeJson
        {
            //get => TypeJsonConverter.ConvertTo(Type);
            set => Type = TypeJsonConverter.ConvertFrom(value);
        }

        [JsonIgnore]
        public RepositoryAccessLevel RepositoryAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="RepositoryAccessLevel" />.
        /// </summary>
        [JsonPropertyName("repository_access_level")]
        public string RepositoryAccessLevelJson
        {
            set => RepositoryAccessLevel = RepositoryAccessLevelJsonConverter.ConvertFrom(value);
        }

        [JsonIgnore]
        public PipelineAccessLevel PipelineAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="PipelineAccessLevel" />.
        /// </summary>
        [JsonPropertyName("pipeline_access_level")]
        public string PipelineAccessLevelJson
        {
            set => PipelineAccessLevel = PipelineAccessLevelJsonConverter.ConvertFrom(value);
        }

        [JsonIgnore]
        public SandboxAccessLevel SandboxAccessLevel { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="SandboxAccessLevel" />.
        /// </summary>
        [JsonPropertyName("sandbox_access_level")]
        public string SandboxAccessLevelJson
        {
            set => SandboxAccessLevel = SandboxAccessLevelJsonConverter.ConvertFrom(value);
        }
    }
}