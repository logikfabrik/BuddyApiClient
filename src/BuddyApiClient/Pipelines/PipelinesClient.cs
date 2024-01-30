namespace BuddyApiClient.Pipelines
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Pipelines.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class PipelinesClient : ClientBase, IPipelinesClient
    {
        public PipelinesClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<PipelineDetails?> Create(Domain domain, ProjectName projectName, CreatePipeline content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines";

            return await HttpClientFacade.Post<PipelineDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task<PipelineDetails?> Get(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}";

            return await HttpClientFacade.Get<PipelineDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<PipelineList?> List(Domain domain, ProjectName projectName, ListPipelinesQuery? query = null, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines{query?.Build()}";

            return await HttpClientFacade.Get<PipelineList>(url, cancellationToken: cancellationToken);
        }

        public ICollectionIterator ListAll(Domain domain, ProjectName projectName, ListPipelinesQuery collectionQuery, CollectionPageResponseHandler<ListPipelinesQuery, PipelineList> collectionPageResponseHandler)
        {
            return new CollectionIterator<ListPipelinesQuery, PipelineList>(async (query, cancellationToken) => await List(domain, projectName, query, cancellationToken), collectionPageResponseHandler, collectionQuery);
        }

        public async Task Delete(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}