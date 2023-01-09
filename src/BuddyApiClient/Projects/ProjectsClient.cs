namespace BuddyApiClient.Projects
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectsClient : ClientBase, IProjectsClient
    {
        public ProjectsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<ProjectDetails?> Create(Domain domain, CreateProject content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects";

            return await HttpClientFacade.Post<ProjectDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task<ProjectDetails?> Get(Domain domain, ProjectName name, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{name}";

            return await HttpClientFacade.Get<ProjectDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<ProjectList?> List(Domain domain, ListProjectsQuery? query = null, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects{query?.Build()}";

            return await HttpClientFacade.Get<ProjectList>(url, cancellationToken: cancellationToken);
        }

        public ICollectionIterator ListAll(Domain domain, ListProjectsQuery collectionQuery, CollectionPageResponseHandler<ListProjectsQuery, ProjectList> collectionPageResponseHandler)
        {
            return new CollectionIterator<ListProjectsQuery, ProjectList>(async (query, cancellationToken) => await List(domain, query, cancellationToken), collectionPageResponseHandler, collectionQuery);
        }

        public async Task Delete(Domain domain, ProjectName name, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{name}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<ProjectDetails?> Update(Domain domain, ProjectName name, UpdateProject content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{name}";

            return await HttpClientFacade.Patch<ProjectDetails>(url, content, cancellationToken: cancellationToken);
        }
    }
}