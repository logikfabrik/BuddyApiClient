namespace BuddyApiClient.Workspaces;

using System;
using System.Threading.Tasks;
using BuddyApiClient.Core;
using BuddyApiClient.Workspaces.Models.Response;
using EnsureThat;

internal sealed class WorkspacesClient : ClientBase, IWorkspacesClient
{
    public WorkspacesClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
    {
    }

    public async Task<WorkspaceDetails?> Get(string domain, CancellationToken cancellationToken = default) => await HttpClientFacade.Get<WorkspaceDetails>($"workspaces/{Ensure.String.IsNotNullOrWhiteSpace(domain, nameof(domain))}", cancellationToken);

    public async Task<WorkspaceList?> GetList(CancellationToken cancellationToken = default) => await HttpClientFacade.Get<WorkspaceList>("workspaces", cancellationToken);
}
