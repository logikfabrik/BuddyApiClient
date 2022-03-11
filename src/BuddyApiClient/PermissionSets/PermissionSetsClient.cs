namespace BuddyApiClient.PermissionSets
{
    using BuddyApiClient.Core;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;
    using EnsureThat;

    internal sealed class PermissionSetsClient : ClientBase, IPermissionSetsClient
    {
        public PermissionSetsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<PermissionSetDetails?> Create(string domain, CreatePermissionSet content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/permissions";

            return await HttpClientFacade.Post<PermissionSetDetails>(url, content, cancellationToken);
        }

        public async Task<PermissionSetDetails?> Get(string domain, int id, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/permissions/{id}";

            return await HttpClientFacade.Get<PermissionSetDetails>(url, cancellationToken);
        }

        public async Task<PermissionSetList?> List(string domain, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/permissions";

            return await HttpClientFacade.Get<PermissionSetList>(url, cancellationToken);
        }

        public async Task<PermissionSetDetails?> Update(string domain, int id, UpdatePermissionSet content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/permissions/{id}";

            return await HttpClientFacade.Patch<PermissionSetDetails>(url, content, cancellationToken);
        }

        public async Task Delete(string domain, int id, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/permissions/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}