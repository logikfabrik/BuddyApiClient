namespace BuddyApiClient.IntegrationTest.Pipelines.Preconditions
{
    using BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Pipelines;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;
    using System.Net;

    internal sealed class PipelineExistsPrecondition : Precondition<PipelineId>
    {
        public PipelineExistsPrecondition(IPipelinesClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp) : base(SetUp(client, domainSetUp, projectSetUp), setUp => TearDown(client, domainSetUp, projectSetUp, setUp))
        {
        }

        private static Func<Task<PipelineId>> SetUp(IPipelinesClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp)
        {
            return async () =>
            {
                var pipeline = await client.Create(await domainSetUp(), await projectSetUp(), CreateOnClickPipelineRequestFactory.Create());

                return pipeline?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IPipelinesClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PipelineId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Delete(await domainSetUp(), await projectSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing.
                }
            };
        }
    }
}