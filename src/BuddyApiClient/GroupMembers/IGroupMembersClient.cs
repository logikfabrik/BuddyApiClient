namespace BuddyApiClient.GroupMembers
{
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    public interface IGroupMembersClient
    {
        Task<MemberDetails?> Add(Domain domain, GroupId groupId, AddGroupMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Get(Domain domain, GroupId groupId, MemberId memberId, CancellationToken cancellationToken = default);

        Task<MemberList?> List(Domain domain, GroupId groupId, CancellationToken cancellationToken = default);

        Task Remove(Domain domain, GroupId groupId, MemberId memberId, CancellationToken cancellationToken = default);
    }
}