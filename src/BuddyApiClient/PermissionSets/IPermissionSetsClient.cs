namespace BuddyApiClient.PermissionSets
{
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    public interface IPermissionSetsClient
    {
        Task<PermissionSetDetails?> Create(Domain domain, CreatePermissionSet content, CancellationToken cancellationToken = default);

        Task<PermissionSetDetails?> Get(Domain domain, PermissionSetId id, CancellationToken cancellationToken = default);

        Task<PermissionSetList?> List(Domain domain, CancellationToken cancellationToken = default);

        Task<PermissionSetDetails?> Update(Domain domain, PermissionSetId id, UpdatePermissionSet content, CancellationToken cancellationToken = default);

        Task Delete(Domain domain, PermissionSetId id, CancellationToken cancellationToken = default);
    }
}