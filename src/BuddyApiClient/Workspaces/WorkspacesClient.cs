namespace BuddyApiClient.Workspaces
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Workspaces.Models;
    using BuddyApiClient.Workspaces.Models.Response;

    internal sealed class WorkspacesClient : ClientBase, IWorkspacesClient
    {
        public WorkspacesClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<WorkspaceDetails?> Get(Domain domain, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}";

            return await HttpClientFacade.Get<WorkspaceDetails>(url, cancellationToken);
        }

        public async Task<WorkspaceList?> List(CancellationToken cancellationToken = default)
        {
            const string url = "workspaces";

            return await HttpClientFacade.Get<WorkspaceList>(url, cancellationToken);
        }
    }
}