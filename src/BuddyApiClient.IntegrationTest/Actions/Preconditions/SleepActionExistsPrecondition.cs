namespace BuddyApiClient.IntegrationTest.Actions.Preconditions
{
    using System.Net;
    using BuddyApiClient.Actions;
    using BuddyApiClient.Actions.Extensions;
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.IntegrationTest.Actions.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Workspaces.Models;

    internal sealed class SleepActionExistsPrecondition : Precondition<ActionId>
    {
        public SleepActionExistsPrecondition(IActionsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PipelineId>> pipelineSetUp) : base(SetUp(client, domainSetUp, projectSetUp, pipelineSetUp), setUp => TearDown(client, domainSetUp, projectSetUp, pipelineSetUp, setUp))
        {
        }

        private static Func<Task<ActionId>> SetUp(IActionsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PipelineId>> pipelineSetUp)
        {
            return async () =>
            {
                var action = await client.Add(await domainSetUp(), await projectSetUp(), await pipelineSetUp(), AddSleepActionRequestFactory.Create());

                return action?.Id ?? throw new PreconditionSetUpException();
            };
        }

        private static Func<Task> TearDown(IActionsClient client, Func<Task<Domain>> domainSetUp, Func<Task<ProjectName>> projectSetUp, Func<Task<PipelineId>> pipelineSetUp, Func<Task<ActionId>> setUp)
        {
            return async () =>
            {
                try
                {
                    await client.Remove(await domainSetUp(), await projectSetUp(), await pipelineSetUp(), await setUp());
                }
                catch (HttpRequestException e) when (e.StatusCode == HttpStatusCode.NotFound)
                {
                    // Do nothing.
                }
            };
        }
    }
}
