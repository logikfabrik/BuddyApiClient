namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;

    internal sealed class MembersClient : ClientBase, IMembersClient
    {
        public MembersClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<MemberDetails?> Add(string domain, AddMember content, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MemberDetails?> Get(string domain, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<MemberList?> List(string domain, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task Remove(string domain, int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}