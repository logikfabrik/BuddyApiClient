namespace BuddyApiClient.Members
{
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;

    public interface IMembersClient
    {
        Task<MemberDetails?> Add(string domain, AddMember content, CancellationToken cancellationToken = default);

        Task<MemberDetails?> Get(string domain, int id, CancellationToken cancellationToken = default);

        Task<MemberList?> List(string domain, CancellationToken cancellationToken = default);

        Task Remove(string domain, int id, CancellationToken cancellationToken = default);
    }
}