namespace BuddyApiClient.IntegrationTest.Actions
{
    using System.Net;
    using BuddyApiClient.Actions.Extensions;
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

                SleepActionDetails? action = null;

                try
                {
                    action = await sut.Add(await domain(), await projectName(), await pipelineId(), AddSleepActionRequestFactory.Create());

                    action.Should().NotBeNull();
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

                action.Should().NotBeNull();
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

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheGroups()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .Add(new SleepActionExistsPrecondition(Fixture.BuddyClient.Actions, domain, projectName, pipelineId))
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var actions = await sut.List(await domain(), await projectName(), await pipelineId());

                actions?.Actions.Should().NotBeEmpty();
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheSleepAction()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .Add(new SleepActionExistsPrecondition(Fixture.BuddyClient.Actions, domain, projectName, pipelineId), out var actionId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var model = UpdateSleepActionRequestFactory.Create();

                var action = await sut.Update(await domain(), await projectName(), await pipelineId(), await actionId(), model);

                action?.Name.Should().Be(model.Name);
            }

            [Fact]
            public async Task Should_Throw_When_TheActionDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var model = UpdateSleepActionRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), await projectName(), await pipelineId(), ActionIdFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_RemoveTheSleepAction()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .Add(new SleepActionExistsPrecondition(Fixture.BuddyClient.Actions, domain, projectName, pipelineId), out var actionId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                await sut.Remove(await domain(), await projectName(), await pipelineId(), await actionId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await pipelineId(), await actionId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_TheActionDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Actions;

                var act = FluentActions.Awaiting(async () => await sut.Remove(await domain(), await projectName(), await pipelineId(), ActionIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}
