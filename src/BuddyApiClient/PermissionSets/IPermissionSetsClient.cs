namespace BuddyApiClient.PermissionSets
{
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;

    public interface IPermissionSetsClient
    {
        Task<PermissionSetDetails?> Create(string domain, CreatePermissionSet content, CancellationToken cancellationToken = default);

        Task<PermissionSetDetails?> Get(string domain, int id, CancellationToken cancellationToken = default);

        Task<PermissionSetList?> List(string domain, CancellationToken cancellationToken = default);

        Task<PermissionSetDetails?> Update(string domain, int id, UpdatePermissionSet content, CancellationToken cancellationToken = default);

        Task Delete(string domain, int id, CancellationToken cancellationToken = default);
    }
}