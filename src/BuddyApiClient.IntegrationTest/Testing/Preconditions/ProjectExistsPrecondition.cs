namespace BuddyApiClient.IntegrationTest.Testing.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectExistsPrecondition : Precondition<ProjectName>
    {
        public ProjectExistsPrecondition(IProjectsClient client, Func<Task<Domain>> domainSetUp, string displayName) : base(SetUp(client, domainSetUp, displayName), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<ProjectName>> SetUp(IProjectsClient client, Func<Task<Domain>> domainSetUp, string displayName)
        {
            return async () =>
            {
                var project = await client.Create(await domainSetUp(), new CreateProject(displayName));

                return project?.Name ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IProjectsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Delete(await domainSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }
    }
}