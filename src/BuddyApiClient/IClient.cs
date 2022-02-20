namespace BuddyApiClient;

using BuddyApiClient.Workspaces;

public interface IClient
{
    public IWorkspacesClient Workspaces { get; }
}
