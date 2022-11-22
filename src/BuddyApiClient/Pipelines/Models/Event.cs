namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record Event
    {
        public Event(EventType type, IEnumerable<string> references)
        {
            Type = type;
            References = Ensure.Any.HasValue(references, nameof(references));
        }

        [JsonIgnore]
        public EventType Type { get; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("type")]
        public string TypeJson => EventTypeJsonConverter.ConvertTo(Type);

        [JsonPropertyName("refs")]
        public IEnumerable<string> References { get; }

        public IEnumerable<string> Tags { get; set; }
    }
}