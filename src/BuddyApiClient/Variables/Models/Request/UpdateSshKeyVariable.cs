namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;

    public sealed record UpdateSshKeyVariable() : UpdateVariable(Type.SshKey)
    {
        [JsonIgnore]
        public FilePlace? FilePlace { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="FilePlace" />.
        /// </summary>
        [JsonPropertyName("file_place")]
        public string? FilePlaceJson => FilePlace is null ? null : FilePlaceJsonConverter.ConvertTo(FilePlace);

        [JsonPropertyName("file_name")]
        public string? FileName { get; set; }

        [JsonPropertyName("file_path")]
        public string? FilePath { get; set; }

        [JsonPropertyName("file_chmod")]
        public FilePermission? FilePermission { get; set; }
    }
}