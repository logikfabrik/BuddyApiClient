namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record CreateSshKeyVariable : CreateVariable
    {
        public CreateSshKeyVariable(string key, string value, FilePlace filePlace, string fileName, string filePath, FilePermission filePermission) : this(new Domain(), key, value, filePlace, fileName, filePath, filePermission)
        {
        }

        public CreateSshKeyVariable(IScope scope, string key, string value, FilePlace filePlace, string fileName, string filePath, FilePermission filePermission) : base(scope, key, value, Type.SshKey)
        {
            FilePlace = filePlace;
            FileName = Ensure.String.IsNotNullOrEmpty(fileName, nameof(fileName));
            FilePath = Ensure.String.IsNotNullOrEmpty(filePath, nameof(filePath));
            FilePermission = filePermission;
        }

        [JsonIgnore]
        public FilePlace FilePlace { get; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="FilePlace" />.
        /// </summary>
        [JsonPropertyName("file_place")]
        public string FilePlaceJson => FilePlaceJsonConverter.ConvertTo(FilePlace);

        [JsonPropertyName("file_name")]
        public string FileName { get; }

        [JsonPropertyName("file_path")]
        public string FilePath { get; }

        [JsonPropertyName("file_chmod")]
        public FilePermission FilePermission { get; }
    }
}