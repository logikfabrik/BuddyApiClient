namespace BuddyApiClient.Actions.Models
{
    using System.Text.Json.Serialization;
    using BuddyApiClient.Projects.Models;

    public sealed record TriggerCondition
    {
        [JsonIgnore]
        public TriggerConditionType Type { get; set; }

        /// <summary>
        ///     For JSON serialization/deserialization. Use property <see cref="Type" />.
        /// </summary>
        [JsonPropertyName("trigger_condition")]
        public string TypeJson => TriggerConditionTypeJsonConverter.ConvertTo(Type);

        [JsonPropertyName("trigger_condition_paths")]
        public IEnumerable<string>? Paths { get; set; }

        [JsonPropertyName("trigger_variable_key")]
        public string? Key { get; set; }

        [JsonPropertyName("trigger_variable_value")]
        public string? Value { get; set; }

        [JsonPropertyName("trigger_hours")]
        public IEnumerable<int>? Hours { get; set; }

        [JsonPropertyName("trigger_days")]
        public IEnumerable<int>? Days { get; set; }

        [JsonPropertyName("zone_id")]
        public TimeZoneId? TimeZoneId { get; set; }

        [JsonPropertyName("trigger_project_name")]
        public ProjectName? ProjectName { get; set; }

        [JsonPropertyName("trigger_pipeline_name")]
        public string? PipelineName { get; set; }
    }
}
