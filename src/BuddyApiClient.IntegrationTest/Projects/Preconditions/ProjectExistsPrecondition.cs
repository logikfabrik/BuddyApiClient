namespace BuddyApiClient.IntegrationTest.Projects.Preconditions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Projects.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class ProjectExistsPrecondition : Precondition<ProjectName>
    {
        public ProjectExistsPrecondition(IProjectsClient client, Func<Task<Domain>> domainSetUp) : base(SetUp(client, domainSetUp), setUp => TearDown(client, domainSetUp, setUp))
        {
        }

        private static Func<Task<ProjectName>> SetUp(IProjectsClient client, Func<Task<Domain>> domainSetUp)
        {
            return async () =>
            {
                var project = await client.Create(await domainSetUp(), CreateProjectRequestFactory.Create());

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
                    // Do nothing.
                }
            };
        }
    }
}