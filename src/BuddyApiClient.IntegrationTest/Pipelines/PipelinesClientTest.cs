namespace BuddyApiClient.IntegrationTest.Pipelines
{
    using System.Collections;
    using System.Net;
    using BuddyApiClient.IntegrationTest.Pipelines.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Pipelines.Preconditions;
    using BuddyApiClient.IntegrationTest.Projects.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Pipelines.Models.Response;

    public sealed class PipelinesClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Theory]
            [ClassData(typeof(TestData))]
            public async Task Should_CreateThePipeline(CreatePipeline content)
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                PipelineDetails? pipeline = null;

                try
                {
                    pipeline = await sut.Create(await domain(), await projectName(), content);

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

            private class TestData : IEnumerable<object[]>
            {
                public IEnumerator<object[]> GetEnumerator()
                {
                    yield return new object[] { CreateOnClickPipelineRequestFactory.Create() };
                    yield return new object[] { CreateOnEventPipelineRequestFactory.Create() };
                    yield return new object[] { CreateOnSchedulePipelineRequestFactory.Create() };
                }

                IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
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

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnThePipelines()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName))
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                var pipelines = await sut.List(await domain(), await projectName());

                pipelines?.Pipelines.Should().NotBeEmpty();
            }
        }

        public sealed class ListAll : BuddyClientTest
        {
            public ListAll(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnThePipelines()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PipelineExistsPrecondition(Fixture.BuddyClient.Pipelines, domain, projectName))
                    .SetUp();

                var sut = Fixture.BuddyClient.Pipelines;

                var pipelines = new List<PipelineSummary>();

                var collectionQuery = new ListPipelinesQuery();

                var collectionIterator = sut.ListAll(await domain(), await projectName(), collectionQuery, (_, response, _) =>
                {
                    pipelines.AddRange(response?.Pipelines ?? Enumerable.Empty<PipelineSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                pipelines.Should().NotBeEmpty();
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
