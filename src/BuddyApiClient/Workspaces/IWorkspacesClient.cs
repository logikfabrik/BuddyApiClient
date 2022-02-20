namespace BuddyApiClient.Workspaces;

using System.Threading.Tasks;
using BuddyApiClient.Workspaces.Models.Response;

public interface IWorkspacesClient
{
    public Task<WorkspaceDetails?> Get(string domain, CancellationToken cancellationToken = default);

    public Task<WorkspaceList?> GetList(CancellationToken cancellationToken = default);
}
