namespace BuddyApiClient.IntegrationTest.Pipelines
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Pipelines.Preconditions;
    using BuddyApiClient.IntegrationTest.Projects.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Pipelines.Models.Response;

    public sealed class PipelinesClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateThePipeline()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                PipelineDetails? pipeline = null;

                try
                {
                    pipeline = await sut.Create(await domain(), await projectName(), CreateOnClickPipelineRequestFactory.Create());

                    pipeline.Should().NotBeNull();
                }
                finally
                {
                    if (pipeline is not null)
                    {
                        await sut.Delete(await domain(), await projectName(), pipeline.Id);
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
            public async Task Should_ReturnThePipeline()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                var pipeline = await sut.Get(await domain(), await projectName(), await pipelineId());

                pipeline.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_ThePipelineDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), PipelineIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_DeleteThePipeline()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName), out var pipelineId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                await sut.Delete(await domain(), await projectName(), await pipelineId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await pipelineId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_ThePipelineDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                var act = FluentActions.Awaiting(async () => await sut.Delete(await domain(), await projectName(), PipelineIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}
