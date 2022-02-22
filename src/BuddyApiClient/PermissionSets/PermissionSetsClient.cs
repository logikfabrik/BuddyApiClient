namespace BuddyApiClient.PermissionSets
{
    using BuddyApiClient.Core;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;

    internal sealed class PermissionSetsClient : ClientBase, IPermissionSetsClient
    {
        public PermissionSetsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<PermissionSetDetails?> Create(string domain, CreatePermissionSet content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionSetDetails?> Get(string domain, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionSetList?> List(string domain, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<PermissionSetDetails?> Update(string domain, int id, UpdatePermissionSet content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(string domain, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}