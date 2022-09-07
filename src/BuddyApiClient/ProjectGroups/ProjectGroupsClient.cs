namespace BuddyApiClient.ProjectGroups
{
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.ProjectGroups.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectGroupsClient : ClientBase, IProjectGroupsClient
    {
        public ProjectGroupsClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<ProjectGroupDetails?> Add(Domain domain, ProjectName projectName, AddProjectGroup content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/groups";

            return await HttpClientFacade.Post<ProjectGroupDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task<ProjectGroupDetails?> Get(Domain domain, ProjectName projectName, GroupId groupId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/groups/{groupId}";

            return await HttpClientFacade.Get<ProjectGroupDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<GroupList?> List(Domain domain, ProjectName projectName, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/groups";

            return await HttpClientFacade.Get<GroupList>(url, cancellationToken: cancellationToken);
        }

        public async Task<ProjectGroupDetails?> Update(Domain domain, ProjectName projectName, GroupId groupId, UpdateProjectGroup content, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/groups/{groupId}";

            return await HttpClientFacade.Patch<ProjectGroupDetails>(url, content, cancellationToken: cancellationToken);
        }

        public async Task Remove(Domain domain, ProjectName projectName, GroupId groupId, CancellationToken cancellationToken = default)
        {
            var url = $"workspaces/{domain}/projects/{projectName}/groups/{groupId}";

            await HttpClientFacade.Delete(url, cancellationToken);
        }
    }
}