namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class MembersClient : ClientBase, IMembersClient
    {
        public MembersClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<MemberDetails?> Add(Domain domain, AddMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members";

            return await HttpClientFacade.Post<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Get(Domain domain, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Get<MemberDetails>(url, cancellationToken);
        }

        public async Task<MemberList?> List(Domain domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public IPageIterator ListAll(Domain domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler)
        {
            return new PageIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public async Task Remove(Domain domain, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<MemberDetails?> Update(Domain domain, MemberId id, UpdateMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Patch<MemberDetails>(url, content, cancellationToken);
        }
    }
}