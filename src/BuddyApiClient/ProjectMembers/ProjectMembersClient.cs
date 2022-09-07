namespace BuddyApiClient.ProjectMembers
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.ProjectMembers.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectMembersClient : ClientBase, IProjectMembersClient
    {
        public ProjectMembersClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<ProjectMemberDetails?> Add(Domain domain, ProjectName projectName, AddProjectMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members";

            return await HttpClientFacade.Post<ProjectMemberDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task<ProjectMemberDetails?> Get(Domain domain, ProjectName projectName, MemberId memberId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{memberId}";

            return await HttpClientFacade.Get<ProjectMemberDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<MemberList?> List(Domain domain, ProjectName projectName, ListMembersQuery? query = default, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members{query?.Build()}";

            return await HttpClientFacade.Get<MemberList>(url, cancellationToken: cancellationToken);
        }

        public ICollectionIterator ListAll(Domain domain, ProjectName projectName, ListMembersQuery collectionQuery, CollectionPageResponseHandler<ListMembersQuery, MemberList> collectionPageResponseHandler)
        {
            return new CollectionIterator<ListMembersQuery, MemberList>(async (query, cancellationToken) => await List(domain, projectName, query, cancellationToken), collectionPageResponseHandler, collectionQuery);
        }

        public async Task Remove(Domain domain, ProjectName projectName, MemberId memberId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{memberId}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }

        public async Task<ProjectMemberDetails?> Update(Domain domain, ProjectName projectName, MemberId memberId, UpdateProjectMember content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/members/{memberId}";

            return await HttpClientFacade.Patch<ProjectMemberDetails>(url, content, cancellationToken: cancellationToken);
        }
    }
}