namespace BuddyApiClient.IntegrationTest.Projects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Projects.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Projects.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class ProjectsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateTheProject()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                ProjectDetails? project = null;

                try
                {
                    project = await sut.Create(await domain(), CreateProjectRequestFactory.Create());

                    project.Should().NotBeNull();
                }
                finally
                {
                    if (project is not null)
                    {
                        await sut.Delete(await domain(), project.Name);
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
            public async Task Should_ReturnTheProject()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var project = await sut.Get(await domain(), await projectName());

                project.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), ProjectNameFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheProjects()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var projects = await sut.List(await domain());

                projects?.Projects.Should().NotBeEmpty();
            }
        }

        public sealed class ListAll : BuddyClientTest
        {
            public ListAll(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheProjects()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var projects = new List<ProjectSummary>();

                var collectionQuery = new ListProjectsQuery();

                var collectionIterator = sut.ListAll(await domain(), collectionQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                projects.Should().NotBeEmpty();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_DeleteTheProject()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                await sut.Delete(await domain(), await projectName());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var act = FluentActions.Awaiting(async () => await sut.Delete(await domain(), ProjectNameFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheProject()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var model = UpdateProjectRequestFactory.Create();

                var project = await sut.Update(await domain(), await projectName(), model);

                project?.DisplayName.Should().Be(model.DisplayName);
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var model = UpdateProjectRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), ProjectNameFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}