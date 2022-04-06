namespace BuddyApiClient.Variables.Models.Response
{
    using System.Text.Json.Serialization;

    public sealed record VariableDetails
    {
        [JsonPropertyName("url")]
        public Uri? Url { get; set; }

        [JsonPropertyName("id")]
        public VariableId Id { get; set; }

        [JsonPropertyName("key")]
        public string? Key { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonIgnore]
        public Type Type { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("type")]
        public string TypeJson
        {
            set => Type = TypeJsonConverter.ConvertFrom(value);
        }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("encrypted")]
        public bool Encrypted { get; set; }

        [JsonIgnore]
        public FilePlace? FilePlace { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="FilePlace" />.
        /// </summary>
        [JsonPropertyName("file_place")]
        public string? FilePlaceJson
        {
            set
            {
                if (value is null)
                {
                    // TODO: Check if needed.
                    return;
                }

                FilePlace = FilePlaceJsonConverter.ConvertFrom(value);
            }
        }

        [JsonPropertyName("file_name")]
        public string? FileName { get; set; }

        [JsonPropertyName("file_path")]
        public string? FilePath { get; set; }

        [JsonPropertyName("file_chmod")]
        public FilePermission? FilePermission { get; set; }

        [JsonPropertyName("public_value")]
        public string? PublicValue { get; set; }

        [JsonPropertyName("key_fingerprint")]
        public string? KeyFingerprint { get; set; }

        [JsonPropertyName("checksum")]
        public string? Checksum { get; set; }
    }
}