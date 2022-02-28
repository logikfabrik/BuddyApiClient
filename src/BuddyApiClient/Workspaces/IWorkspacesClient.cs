namespace BuddyApiClient.Workspaces
{
    using BuddyApiClient.Workspaces.Models.Response;

    public interface IWorkspacesClient
    {
        Task<WorkspaceDetails?> Get(string domain, CancellationToken cancellationToken = default);

        Task<WorkspaceList?> List(CancellationToken cancellationToken = default);
    }
}