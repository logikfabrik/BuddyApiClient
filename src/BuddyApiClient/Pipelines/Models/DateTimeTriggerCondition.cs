namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;

    public sealed record DateTimeTriggerCondition() : TriggerCondition(TriggerConditionType.DateTime)
    {
        // TODO: Improve representation
        [JsonPropertyName("trigger_hours")]
        public IEnumerable<int>? Hours { get; set; }

        // TODO: Improve representation
        [JsonPropertyName("trigger_days")]
        public IEnumerable<int>? Days { get; set; }

        [JsonPropertyName("zone_id")]
        public TimeZoneId? TimeZoneId { get; set; }
    }
}