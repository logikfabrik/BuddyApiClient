namespace BuddyApiClient.Variables.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public record CreateVariable
    {
        public CreateVariable(string key, string value) : this(new Domain(), key, value, Type.Variable)
        {
        }

        public CreateVariable(IScope scope, string key, string value) : this(scope, key, value, Type.Variable)
        {
        }

        protected CreateVariable(IScope scope, string key, string value, Type type)
        {
            Key = Ensure.String.IsNotNullOrEmpty(key, nameof(key));
            Value = Ensure.String.IsNotNullOrEmpty(value, nameof(value));
            Type = type;

            switch (scope)
            {
                case Domain:
                    // Do nothing.
                    break;
                case Project project:
                    Project = project;
                    break;
                case Pipeline pipeline:
                    Pipeline = pipeline;
                    break;
                case Action action:
                    Action = action;
                    break;
                default: throw new NotSupportedException();
            }
        }

        [JsonPropertyName("key")]
        public string Key { get; }

        [JsonPropertyName("value")]
        public string Value { get; }

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
        public bool Settable { get; set; }

        [JsonPropertyName("encrypted")]
        public bool Encrypted { get; set; }

        [JsonPropertyName("project")]
        public Project? Project { get; }

        [JsonPropertyName("pipeline")]
        public Pipeline? Pipeline { get; }

        [JsonPropertyName("action")]
        public Action? Action { get; }
    }
}