namespace BuddyApiClient.Actions
{
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Actions.Models.Request;
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IActionsClient
    {
        Task<ActionDetails?> Add(Domain domain, ProjectName projectName, PipelineId pipelineId, AddAction content, CancellationToken cancellationToken = default);

        Task<ActionDetails?> Get(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default);

        Task<ActionList?> List(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default);

        Task<ActionDetails?> Update(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, UpdateAction content, CancellationToken cancellationToken = default);

        Task Remove(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default);
    }
}