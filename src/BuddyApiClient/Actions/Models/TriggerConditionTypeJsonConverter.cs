namespace BuddyApiClient.Actions.Models
{
    internal static class TriggerConditionTypeJsonConverter
    {
        public const string AlwaysAsJson = "ALWAYS";
        public const string OnChangeAsJson = "ON_CHANGE";
        public const string OnChangeAtPathAsJson = "ON_CHANGE_AT_PATH";
        public const string VariableIsAsJson = "VAR_IS";
        public const string VariableIsNotAsJson = "VAR_IS_NOT";
        public const string VariableContainsAsJson = "VAR_CONTAINS";
        public const string VariableContainsNotAsJson = "VAR_NOT_CONTAINS";
        public const string DateTimeAsJson = "DATETIME";
        public const string SuccessPipelineAsJson = "SUCCESS_PIPELINE";

        public static TriggerConditionType ConvertFrom(string? json)
        {
            return json switch
            {
                AlwaysAsJson => TriggerConditionType.Always,
                OnChangeAsJson => TriggerConditionType.OnChange,
                OnChangeAtPathAsJson => TriggerConditionType.OnChangeAtPath,
                VariableIsAsJson => TriggerConditionType.VariableIs,
                VariableIsNotAsJson => TriggerConditionType.VariableIsNot,
                VariableContainsAsJson => TriggerConditionType.VariableContains,
                VariableContainsNotAsJson => TriggerConditionType.VariableContainsNot,
                DateTimeAsJson => TriggerConditionType.DateTime,
                SuccessPipelineAsJson => TriggerConditionType.SuccessPipeline,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(TriggerConditionType? enumValue)
        {
            return enumValue switch
            {
                TriggerConditionType.Always => AlwaysAsJson,
                TriggerConditionType.OnChange => OnChangeAsJson,
                TriggerConditionType.OnChangeAtPath => OnChangeAtPathAsJson,
                TriggerConditionType.VariableIs => VariableIsAsJson,
                TriggerConditionType.VariableIsNot => VariableIsNotAsJson,
                TriggerConditionType.VariableContains => VariableContainsAsJson,
                TriggerConditionType.VariableContainsNot => VariableContainsNotAsJson,
                TriggerConditionType.DateTime => DateTimeAsJson,
                TriggerConditionType.SuccessPipeline => SuccessPipelineAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}
