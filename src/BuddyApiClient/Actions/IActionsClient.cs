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
        Task<T2?> Add<T1, T2>(Domain domain, ProjectName projectName, PipelineId pipelineId, T1 content, CancellationToken cancellationToken = default) where T1 : AddAction where T2 : ActionDetails;

        Task<ActionDetails?> Get(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default);

        Task<ActionList?> List(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default);

        Task<T2?> Update<T1, T2>(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, T1 content, CancellationToken cancellationToken = default) where T1 : UpdateAction where T2 : ActionDetails;
        
        Task Remove(Domain domain, ProjectName projectName, PipelineId pipelineId, ActionId id, CancellationToken cancellationToken = default);
    }
}