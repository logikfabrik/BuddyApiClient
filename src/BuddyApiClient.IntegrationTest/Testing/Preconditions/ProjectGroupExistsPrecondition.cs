namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectGroupExistsPrecondition : Precondition<GroupId>
    {
        public ProjectGroupExistsPrecondition(IProjectGroupsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PermissionSetId>> permissionSetSetUp, Func<Task<GroupId>> groupSetUp) : base(SetUp(client, domainSetUp, projectSetUp, permissionSetSetUp, groupSetUp), setUp => TearDown(client, domainSetUp, projectSetUp, setUp))
        {
        }

        private static Func<Task<GroupId>> SetUp(IProjectGroupsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PermissionSetId>> permissionSetSetUp, Func<Task<GroupId>> groupSetUp)
        {
            return async () =>
            {
                var group = await client.Add(await domainSetUp(), await projectSetUp(), new AddProjectGroup(new PermissionSet { Id = await permissionSetSetUp() }) { GroupId = await groupSetUp() });

                return group?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IProjectGroupsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<GroupId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Remove(await domainSetUp(), await projectSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }
    }
}