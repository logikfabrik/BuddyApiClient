namespace BuddyApiClient.ProjectMembers
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.ProjectMembers.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IProjectMembersClient
    {
        Task<ProjectMemberDetails?> Add(Domain domain, ProjectName projectName, AddProjectMember content, CancellationToken cancellationToken = default);

        Task<ProjectMemberDetails?> Get(Domain domain, ProjectName projectName, MemberId memberId, CancellationToken cancellationToken = default);

        Task<MemberList?> List(Domain domain, ProjectName projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        ICollectionIterator ListAll(Domain domain, ProjectName projectName, ListMembersQuery collectionQuery, CollectionPageResponseHandler<ListMembersQuery, MemberList> collectionPageResponseHandler);

        Task Remove(Domain domain, ProjectName projectName, MemberId memberId, CancellationToken cancellationToken = default);

        Task<ProjectMemberDetails?> Update(Domain domain, ProjectName projectName, MemberId memberId, UpdateProjectMember content, CancellationToken cancellationToken = default);
    }
}