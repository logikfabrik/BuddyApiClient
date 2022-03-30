namespace BuddyApiClient.Members
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Projects.Models;
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

        public async Task<ProjectMemberDetails?> Add(Domain domain, ProjectName projectName, AddProjectMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members";

            return await HttpClientFacade.Post<ProjectMemberDetails>(url, content, cancellationToken);
        }

        public async Task<MemberDetails?> Get(Domain domain, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Get<MemberDetails>(url, cancellationToken);
        }

        public async Task<ProjectMemberDetails?> Get(Domain domain, ProjectName projectName, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{id}";

            return await HttpClientFacade.Get<ProjectMemberDetails>(url, cancellationToken);
        }

        public async Task<MemberList?> List(Domain domain, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public async Task<MemberList?> List(Domain domain, ProjectName projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken);
        }

        public IPageIterator ListAll(Domain domain, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler)
        {
            return new PageIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public IPageIterator ListAll(Domain domain, ProjectName projectName, ListMembersQuery pageQuery, PageResponseHandler<ListMembersQuery, MemberList> pageResponseHandler)
        {
            return new PageIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, projectName, query, cancellationToken), pageResponseHandler, pageQuery);
        }

        public async Task Remove(Domain domain, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task Remove(Domain domain, ProjectName projectName, MemberId id, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{id}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<MemberDetails?> Update(Domain domain, MemberId id, UpdateMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/members/{id}";

            return await HttpClientFacade.Patch<MemberDetails>(url, content, cancellationToken);
        }

        public async Task<ProjectMemberDetails?> Update(Domain domain, ProjectName projectName, MemberId id, UpdateProjectMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{id}";

            return await HttpClientFacade.Patch<ProjectMemberDetails>(url, content, cancellationToken);
        }
    }
}