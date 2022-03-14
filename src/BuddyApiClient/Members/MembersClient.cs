namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using EnsureThat;

    internal sealed class MembersClient : ClientBase, IMembersClient
    {
        // TODO: See https://buddy.works/docs/api/general/projects/manage-access.

        public MembersClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<MemberDetails?> Add(string domain, AddMember content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/members";

            return await HttpClientFacade.Post<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Add(string domain, string projectName, AddProjectMember content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(projectName, nameof(projectName));

            var url = $"workspaces/{domain}/projects/{projectName}/members";

            return await HttpClientFacade.Post<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Get(string domain, int id, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Get<MemberDetails>(url, cancellationToken);
        }

        public async Task<MemberList?> List(string domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public async Task<MemberList?> List(string domain, string projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(projectName, nameof(projectName));

            var url = $"workspaces/{domain}/projects/{projectName}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public IPageIterator ListAll(string domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler)
        {
            return new PageIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public IPageIterator ListAll(string domain, string projectName, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler)
        {
            return new PageIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, projectName, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public async Task Remove(string domain, int id, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/members/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task Remove(string domain, string projectName, int id, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(projectName, nameof(projectName));

            var url = $"workspaces/{domain}/projects/{projectName}/members/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<MemberDetails?> Update(string domain, int id, UpdateMember content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));

            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Patch<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Update(string domain, string projectName, int id, UpdateProjectMember content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrEmpty(domain, nameof(domain));
            Ensure.String.IsNotNullOrEmpty(projectName, nameof(projectName));

            var url = $"workspaces/{domain}/projects/{projectName}/members/{id}";

            return await HttpClientFacade.Patch<MemberDetails>(url, content, cancellationToken);
        }
    }
}