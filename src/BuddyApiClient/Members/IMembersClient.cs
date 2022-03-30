namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IMembersClient
    {
        Task<MemberDetails?> Add(Domain domain, AddMember content, CancellationToken cancellationToken = default);

        Task<ProjectMemberDetails?> Add(Domain domain, ProjectName projectName, AddProjectMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Get(Domain domain, MemberId id, CancellationToken cancellationToken = default);

        Task<ProjectMemberDetails?> Get(Domain domain, ProjectName projectName, MemberId id, CancellationToken cancellationToken = default);

        Task<MemberList?> List(Domain domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        Task<MemberList?> List(Domain domain, ProjectName projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        IPageIterator ListAll(Domain domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler);

        IPageIterator ListAll(Domain domain, ProjectName projectName, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler);

        Task Remove(Domain domain, MemberId id, CancellationToken cancellationToken = default);

        Task Remove(Domain domain, ProjectName projectName, MemberId id, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Update(Domain domain, MemberId id, UpdateMember content, CancellationToken cancellationToken = default);

        Task<ProjectMemberDetails?> Update(Domain domain, ProjectName projectName, MemberId id, UpdateProjectMember content, CancellationToken cancellationToken = default);
    }
}