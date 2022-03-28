namespace BuddyApiClient.IntegrationTest
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
        private readonly Func<Task> _tearDown;

        public ProjectExistsPrecondition(IProjectsClient client, Precondition<Domain> domainExistsPrecondition, string displayName) : base(SetUp(client, domainExistsPrecondition, displayName))
        {
            _tearDown = async () =>
            {
                try
                {
                    await client.Delete(await domainExistsPrecondition.SetUp(), await SetUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing
                }
            };
        }

        private static Func<Task<ProjectName>> SetUp(IProjectsClient client, Precondition<Domain> domainExistsPrecondition, string displayName)
        {
            return async () =>
            {
                var project = await client.Create(await domainExistsPrecondition.SetUp(), new CreateProject(displayName));

                return project?.Name ?? throw new PreconditionSetUpException();
            };
        }

        public override async Task TearDown()
        {
            await base.TearDown();

            if (!IsSetUp)
            {
                return;
            }

            await _tearDown();
        }
    }
}