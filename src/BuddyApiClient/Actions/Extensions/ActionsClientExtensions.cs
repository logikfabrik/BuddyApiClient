namespace BuddyApiClient.Actions.Extensions
{
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Actions.Models.Request;
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public static class ActionsClientExtensions
    {
        public static async Task<SleepActionDetails?> Add(this IActionsClient client, Domain domain, ProjectName projectName, PipelineId pipelineId, AddSleepAction content, CancellationToken cancellationToken = default)
        {
            return await client.Add<AddSleepAction, SleepActionDetails>(domain, projectName, pipelineId, content, cancellationToken);
        }

        public static async Task<SleepActionDetails?> Update(this IActionsClient client, Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, UpdateSleepAction content, CancellationToken cancellationToken = default)
        {
            return await client.Update<UpdateSleepAction, SleepActionDetails>(domain, projectName, pipelineId, id, content, cancellationToken);
        }
    }
}
