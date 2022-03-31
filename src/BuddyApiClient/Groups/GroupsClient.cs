namespace BuddyApiClient.Groups
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Request;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class GroupsClient : ClientBase, IGroupsClient
    {
        public GroupsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<GroupDetails?> Create(Domain domain, CreateGroup content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups";

            return await HttpClientFacade.Post<GroupDetails>(url, content, cancellationToken);
        }

        public async Task<GroupDetails?> Get(Domain domain, GroupId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{id}";

            return await HttpClientFacade.Get<GroupDetails>(url, cancellationToken);
        }

        public async Task<GroupList?> List(Domain domain, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups";

            return await HttpClientFacade.Get<GroupList>(url, cancellationToken);
        }

        public async Task<GroupDetails?> Update(Domain domain, GroupId id, UpdateGroup content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{id}";

            return await HttpClientFacade.Patch<GroupDetails>(url, content, cancellationToken);
        }

        public async Task Delete(Domain domain, GroupId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}