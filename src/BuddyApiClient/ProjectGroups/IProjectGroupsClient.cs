namespace BuddyApiClient.ProjectGroups
{
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.ProjectGroups.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    public interface IProjectGroupsClient
    {
        Task<ProjectGroupDetails?> Add(Domain domain, ProjectName projectName, AddProjectGroup content, CancellationToken cancellationToken = default);

        Task<ProjectGroupDetails?> Get(Domain domain, ProjectName projectName, GroupId groupId, CancellationToken cancellationToken = default);

        Task<GroupList?> List(Domain domain, ProjectName projectName, CancellationToken cancellationToken = default);

        Task<ProjectGroupDetails?> Update(Domain domain, ProjectName projectName, GroupId groupId, UpdateProjectGroup content, CancellationToken cancellationToken = default);

        Task Remove(Domain domain, ProjectName projectName, GroupId groupId, CancellationToken cancellationToken = default);
    }
}