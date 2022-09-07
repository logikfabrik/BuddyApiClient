namespace BuddyApiClient.IntegrationTest.Actions
{
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.IntegrationTest.Actions.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Pipelines.Preconditions;
    using BuddyApiClient.IntegrationTest.Projects.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;

    public sealed class ActionsClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            public Add(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateTheSleepAction()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                ActionDetails? action = null;

                try
                {
                    action = await sut.Add(await domain(), await projectName(), await pipelineId(), AddSleepActionRequestFactory.Create());

                    action.Should().BeOfType<SleepActionDetails>();
                }
                finally
                {
                    if (action is not null)
                    {
                        await sut.Remove(await domain(), await projectName(), await pipelineId(), action.Id);
                    }
                }
            }

        }
    }
}
