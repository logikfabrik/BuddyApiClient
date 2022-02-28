namespace BuddyApiClient.Workspaces
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Workspaces.Models.Response;
    using EnsureThat;

    internal sealed class WorkspacesClient : ClientBase, IWorkspacesClient
    {
        public WorkspacesClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<WorkspaceDetails?> Get(string domain, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(domain, nameof(domain));

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