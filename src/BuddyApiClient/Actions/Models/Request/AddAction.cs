namespace BuddyApiClient.Actions.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    /// <remarks>
    ///     Implemented as a abstract class, with a constructor overload taking the type as JSON, to allow for user-defined
    ///     actions supported by the API, but not yet by the client.
    /// </remarks>
    public abstract record AddAction
    {
        protected AddAction(string name, Type type) : this(name, TypeJsonConverter.ConvertTo(type))
        {
        }

        protected AddAction(string name, string typeJson)
        {
            Name = Ensure.String.IsNotNullOrEmpty(name, nameof(name));
            TypeJson = Ensure.String.IsNotNullOrEmpty(typeJson, nameof(typeJson));
        }

        [JsonPropertyName("name")]
        public string Name { get; }

        [JsonIgnore]
        public Type Type => TypeJsonConverter.ConvertFrom(TypeJson);

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("type")]
        public string TypeJson { get; }

        [JsonIgnore]
        public TriggerTime TriggerTime { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="TriggerTime" />.
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public string TriggerTimeJson => TriggerTimeJsonConverter.ConvertTo(TriggerTime);
    }
}