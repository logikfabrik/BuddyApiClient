namespace BuddyApiClient.Actions.Models.Request
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public abstract record UpdateAction
    {
        private string? _name;

        [JsonPropertyName("name")]
        public string? Name
        {
            get => _name;
            set
            {
                if (value is null)
                {
                    _name = null;

                    return;
                }

                _name = Ensure.String.IsNotEmpty(value, nameof(value));
            }
        }

        [JsonIgnore]
        public TriggerTime? TriggerTime { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="TriggerTime" />.
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public string? TriggerTimeJson => TriggerTime is null ? null : TriggerTimeJsonConverter.ConvertTo(TriggerTime);
    }
}