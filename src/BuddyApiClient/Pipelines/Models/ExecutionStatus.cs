namespace BuddyApiClient.Pipelines.Models
{
    public enum ExecutionStatus
    {
        Successful,

        Failed,

        InProgress,

        Enqueued,

        Skipped,

        Terminated,

        Initial
    }
}