namespace BuddyApiClient.Pipelines.Models
{
    internal static class ExecutionStatusJsonConverter
    {
        public const string SuccessfulAsJson = "SUCCESSFUL";
        public const string FailedAsJson = "FAILED";
        public const string InProgressAsJson = "INPROGRESS";
        public const string EnqueuedAsJson = "ENQUEUED";
        public const string SkippedAsJson = "SKIPPED";
        public const string TerminatedAsJson = "TERMINATED";
        public const string InitialAsJson = "INITIAL";

        public static ExecutionStatus ConvertFrom(string? json)
        {
            return json switch
            {
                SuccessfulAsJson => ExecutionStatus.Successful,
                FailedAsJson => ExecutionStatus.Failed,
                InProgressAsJson => ExecutionStatus.InProgress,
                EnqueuedAsJson => ExecutionStatus.Enqueued,
                SkippedAsJson => ExecutionStatus.Skipped,
                TerminatedAsJson => ExecutionStatus.Terminated,
                InitialAsJson => ExecutionStatus.Initial,
                _ => throw new NotSupportedException()
            };
        }

        public static string ConvertTo(ExecutionStatus? enumValue)
        {
            return enumValue switch
            {
                ExecutionStatus.Successful => SuccessfulAsJson,
                ExecutionStatus.Failed => FailedAsJson,
                ExecutionStatus.InProgress => InProgressAsJson,
                ExecutionStatus.Enqueued => EnqueuedAsJson,
                ExecutionStatus.Skipped => SkippedAsJson,
                ExecutionStatus.Terminated => TerminatedAsJson,
                ExecutionStatus.Initial => InitialAsJson,
                _ => throw new NotSupportedException()
            };
        }
    }
}