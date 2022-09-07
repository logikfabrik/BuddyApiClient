namespace BuddyApiClient.Pipelines
{
    using BuddyApiClient.Core;
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

        public async Task Delete(Domain domain, ProjectName projectName, PipelineId pipelineId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/pipelines/{pipelineId}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}