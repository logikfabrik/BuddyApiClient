namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;

    public abstract record TriggerCondition([property: JsonIgnore] TriggerConditionType Type)
    {
        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("trigger_condition")]
        public string TypeJson => TriggerConditionTypeJsonConverter.ConvertTo(Type);
    }
}