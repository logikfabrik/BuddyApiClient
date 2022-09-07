namespace BuddyApiClient.Pipelines.Models
{
    using System.Text.Json.Serialization;
    using EnsureThat;

    public sealed record VariableTriggerCondition : TriggerCondition
    {
        private VariableTriggerCondition(TriggerConditionType type, string key, string value) : base(type)
        {
            Key = Ensure.String.IsNotNullOrEmpty(key, nameof(key));
            Value = Ensure.String.IsNotNullOrEmpty(value, nameof(value));
        }

        [JsonPropertyName("trigger_variable_key")]
        public string Key { get; }

        [JsonPropertyName("trigger_variable_value")]
        public string Value { get; }

        public static VariableTriggerCondition CreateConditionForWhenVariableIs(string key, string value)
        {
            return new VariableTriggerCondition(TriggerConditionType.VariableIs, key, value);
        }

        public static VariableTriggerCondition CreateConditionForWhenVariableIsNot(string key, string value)
        {
            return new VariableTriggerCondition(TriggerConditionType.VariableIsNot, key, value);
        }

        public static VariableTriggerCondition CreateConditionForWhenVariableContains(string key, string value)
        {
            return new VariableTriggerCondition(TriggerConditionType.VariableContains, key, value);
        }

        public static VariableTriggerCondition CreateConditionForWhenVariableContainsNot(string key, string value)
        {
            return new VariableTriggerCondition(TriggerConditionType.VariableContainsNot, key, value);
        }
    }
}