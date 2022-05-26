namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;

    public record UpdateVariable
    {
        public UpdateVariable() : this(Type.Variable)
        {
        }

        protected UpdateVariable(Type type)
        {
            Type = type;
        }

        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonIgnore]
        public Type Type { get; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("type")]
        public string TypeJson => TypeJsonConverter.ConvertTo(Type);

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("settable")]
        public bool? Settable { get; set; }

        [JsonPropertyName("encrypted")]
        public bool? Encrypted { get; set; }
    }
}