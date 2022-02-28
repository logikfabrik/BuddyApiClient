namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;

    public interface IMembersClient
    {
        Task<MemberDetails?> Add(string domain, AddMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Get(string domain, int id, CancellationToken cancellationToken = default);

        Task<MemberList?> List(string domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default);

        IPageIterator ListAll(string domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler);

        Task Remove(string domain, int id, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Update(string domain, int id, UpdateMember content, CancellationToken cancellationToken = default);
    }
}