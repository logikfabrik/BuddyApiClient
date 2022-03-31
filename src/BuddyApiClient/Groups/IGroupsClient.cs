namespace BuddyApiClient.Groups
{
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Request;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    public interface IGroupsClient
    {
        Task<GroupDetails?> Create(Domain domain, CreateGroup content, CancellationToken cancellationToken = default);

        Task<GroupDetails?> Get(Domain domain, GroupId id, CancellationToken cancellationToken = default);

        Task<GroupList?> List(Domain domain, CancellationToken cancellationToken = default);

        Task<GroupDetails?> Update(Domain domain, GroupId id, UpdateGroup content, CancellationToken cancellationToken = default);

        Task Delete(Domain domain, GroupId id, CancellationToken cancellationToken = default);
    }
}