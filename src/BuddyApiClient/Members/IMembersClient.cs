namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;

    public interface IMembersClient
    {
        Task<MemberDetails?> Add(string domain, AddMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Add(string domain, string projectName, AddProjectMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Get(string domain, int id, CancellationToken cancellationToken = default);

        Task<MemberList?> List(string domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        Task<MemberList?> List(string domain, string projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        IPageIterator ListAll(string domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler);

        IPageIterator ListAll(string domain, string projectName, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler);

        Task Remove(string domain, int id, CancellationToken cancellationToken = default);

        Task Remove(string domain, string projectName, int id, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Update(string domain, int id, UpdateMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Update(string domain, string projectName, int id, UpdateProjectMember content, CancellationToken cancellationToken = default);
    }
}