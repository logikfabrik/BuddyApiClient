namespace BuddyApiClient.Projects
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using EnsureThat;

    internal sealed class ProjectsClient : ClientBase, IProjectsClient
    {
        public ProjectsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<ProjectDetails?> Create(string domain, CreateProject content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/projects";

            return await HttpClientFacade.Post<ProjectDetails>(url, content, cancellationToken);
        }

        public async Task<ProjectDetails?> Get(string domain, string name, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(name, nameof(name));

            var url = $"workspaces/{domain}/projects/{name}";

            return await HttpClientFacade.Get<ProjectDetails>(url, cancellationToken);
        }

        public async Task<ProjectList?> List(string domain, ListProjectsQuery? query = default, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/projects{query?.Build()}";

            return await HttpClientFacade.Get<ProjectList>(url, cancellationToken);
        }

        public IPageIterator ListAll(string domain, ListProjectsQuery pageQuery, PageResponseHandler<ListProjectsQuery, ProjectList> pageResponseHandler)
        {
            return new PageIterator<ListProjectsQuery, ProjectList>(async (query, cancellationToken) => await List(domain, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public async Task Delete(string domain, string name, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(name, nameof(name));

            var url = $"workspaces/{domain}/projects/{name}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<ProjectDetails?> Update(string domain, string name, UpdateProject content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(name, nameof(name));

            var url = $"workspaces/{domain}/projects/{name}";

            return await HttpClientFacade.Patch<ProjectDetails>(url, content, cancellationToken);
        }
    }
}