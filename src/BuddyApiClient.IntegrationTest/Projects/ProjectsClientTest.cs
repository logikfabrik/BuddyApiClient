namespace BuddyApiClient.IntegrationTest.Projects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
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
            public async Task Should_Create_And_Return_The_Project()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                ProjectDetails? project = null;

                try
                {
                    project = await sut.Create(await domain(), CreateProjectFactory.Create());

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
            public async Task Should_Return_The_Project_If_It_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var project = await sut.Get(await domain(), await projectName());

                project.Should().NotBeNull();
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Projects_If_Any_Exists()
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
            public async Task Should_Return_Projects_If_Any_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Projects;

                var projects = new List<ProjectSummary>();

                var pageQuery = new ListProjectsQuery();

                var pageIterator = sut.ListAll(await domain(), pageQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                projects.Should().NotBeEmpty();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Delete_The_Project_And_Return_Nothing()
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
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Project()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .SetUp();

                var newDisplayName = new Lorem().Word();

                var sut = Fixture.BuddyClient.Projects;

                var project = await sut.Update(await domain(), await projectName(), new UpdateProject { DisplayName = newDisplayName });

                project?.DisplayName.Should().Be(newDisplayName);
            }
        }
    }
}