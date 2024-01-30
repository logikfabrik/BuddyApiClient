namespace BuddyApiClient.PermissionSets
{
    using BuddyApiClient.Core;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class PermissionSetsClient : ClientBase, IPermissionSetsClient
    {
        public PermissionSetsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<PermissionSetDetails?> Create(Domain domain, CreatePermissionSet content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/permissions";

            return await HttpClientFacade.Post<PermissionSetDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task<PermissionSetDetails?> Get(Domain domain, PermissionSetId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/permissions/{id}";

            return await HttpClientFacade.Get<PermissionSetDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<PermissionSetList?> List(Domain domain, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/permissions";

            return await HttpClientFacade.Get<PermissionSetList>(url, cancellationToken: cancellationToken);
        }

        public async Task<PermissionSetDetails?> Update(Domain domain, PermissionSetId id, UpdatePermissionSet content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/permissions/{id}";

            return await HttpClientFacade.Patch<PermissionSetDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task Delete(Domain domain, PermissionSetId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/permissions/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}