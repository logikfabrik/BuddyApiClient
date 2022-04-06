namespace BuddyApiClient.IntegrationTest.ProjectMembers.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectMemberExistsPrecondition : Precondition<MemberId>
    {
        public ProjectMemberExistsPrecondition(IProjectMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PermissionSetId>> permissionSetSetUp, Func<Task<MemberId>> memberSetUp) : base(SetUp(client, domainSetUp, projectSetUp, permissionSetSetUp, memberSetUp), setUp => TearDown(client, domainSetUp, projectSetUp, setUp))
        {
        }

        private static Func<Task<MemberId>> SetUp(IProjectMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PermissionSetId>> permissionSetSetUp, Func<Task<MemberId>> memberSetUp)
        {
            return async () =>
            {
                var member = await client.Add(await domainSetUp(), await projectSetUp(), new AddProjectMember(new PermissionSet { Id = await permissionSetSetUp() }) { MemberId = await memberSetUp() });

                return member?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IProjectMembersClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<MemberId>> setUp)
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