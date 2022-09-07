namespace BuddyApiClient.Pipelines
{
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Pipelines.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IPipelinesClient
    {
        Task<PipelineDetails?> Create(Domain domain, ProjectName projectName, CreatePipeline content, CancellationToken cancellationToken = default);

        Task Delete(Domain domain, ProjectName projectName,PipelineId pipelineId, CancellationToken cancellationToken = default);
    }
}