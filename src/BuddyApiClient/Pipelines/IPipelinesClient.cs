namespace BuddyApiClient.Pipelines
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Pipelines.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IPipelinesClient
    {
        Task<PipelineDetails?> Create(Domain domain, ProjectName projectName, CreatePipeline content, CancellationToken cancellationToken = default);

        Task<PipelineDetails?> Get(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default);

        Task<PipelineList?> List(Domain domain, ProjectName projectName, ListPipelinesQuery? query = null, CancellationToken cancellationToken = default);

        ICollectionIterator ListAll(Domain domain, ProjectName projectName, ListPipelinesQuery collectionQuery, CollectionPageResponseHandler<ListPipelinesQuery, PipelineList> collectionPageResponseHandler);

        Task Delete(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default);
    }
}