namespace BuddyApiClient.Actions.Models
{
    internal static class TriggerTimeJsonConverter
    {
        public const string OnEveryExecutionAsJson = "ON_EVERY_EXECUTION";
        public const string OnFailureAsJson = "ON_FAILURE";
        public const string OnWarningAsJson = "ON_WARNING";
        public const string OnWaitForApproveAsJson = "ON_WAIT_FOR_APPROVE";

        public static TriggerTime ConvertFrom(string? json)
        {
            return json switch
            {
                OnEveryExecutionAsJson => TriggerTime.OnEveryExecution,
                OnFailureAsJson => TriggerTime.OnFailure,
                OnWarningAsJson => TriggerTime.OnWarning,
                OnWaitForApproveAsJson => TriggerTime.OnWaitForApprove,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(TriggerTime? enumValue)
        {
            return enumValue switch
            {
                TriggerTime.OnEveryExecution => OnEveryExecutionAsJson,
                TriggerTime.OnFailure => OnFailureAsJson,
                TriggerTime.OnWarning => OnWarningAsJson,
                TriggerTime.OnWaitForApprove => OnWaitForApproveAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}
