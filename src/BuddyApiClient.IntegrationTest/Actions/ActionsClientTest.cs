namespace BuddyApiClient.IntegrationTest.Actions
{
    using System.Net;
    using BuddyApiClient.Actions.Models.Response;
    using BuddyApiClient.IntegrationTest.Actions.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Actions.Preconditions;
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

        public sealed class Get : BuddyClientTest
        {
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheSleepAction()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .Add(new SleepActionExistsPrecondition(Fixture.BuddyClient.Actions, domain, projectName, pipelineId), out var actionId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var action = await sut.Get(await domain(), await projectName(), await pipelineId(), await actionId());

                action.Should().BeOfType<SleepActionDetails>();
            }

            [Fact]
            public async Task Should_Throw_When_TheSleepActionDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await pipelineId(), ActionIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}
