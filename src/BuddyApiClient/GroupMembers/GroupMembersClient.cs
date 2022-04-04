namespace BuddyApiClient.GroupMembers
{
    using BuddyApiClient.Core;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class GroupMembersClient : ClientBase, IGroupMembersClient
    {
        public GroupMembersClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<MemberDetails?> Add(Domain domain, GroupId groupId, AddGroupMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{groupId}/members";

            return await HttpClientFacade.Post<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Get(Domain domain, GroupId groupId, MemberId memberId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{groupId}/members/{memberId}";

            return await HttpClientFacade.Get<MemberDetails>(url, cancellationToken);
        }

        public async Task<MemberList?> List(Domain domain, GroupId groupId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{groupId}/members";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public async Task Remove(Domain domain, GroupId groupId, MemberId memberId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/groups/{groupId}/members/{memberId}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}