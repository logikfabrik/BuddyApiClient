namespace BuddyApiClient.Variables
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Variables.Models.Request;
    using BuddyApiClient.Variables.Models.Response;

    internal sealed class VariablesClient : ClientBase, IVariablesClient
    {
        public VariablesClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<VariableDetails?> Create(Workspaces.Models.Domain domain, CreateVariable content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/variables";

            return await HttpClientFacade.Post<VariableDetails>(url, content, cancellationToken);
        }

        public async Task<VariableDetails?> Get(Workspaces.Models.Domain domain, VariableId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/variables/{id}";

            return await HttpClientFacade.Get<VariableDetails>(url, cancellationToken);
        }

        public async Task Delete(Workspaces.Models.Domain domain, VariableId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/variables/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}