namespace BuddyApiClient.IntegrationTest.ProjectGroups
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Groups.Preconditions;
    using BuddyApiClient.IntegrationTest.PermissionSets.Preconditions;
    using BuddyApiClient.IntegrationTest.ProjectGroups.Preconditions;
    using BuddyApiClient.IntegrationTest.Projects.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.ProjectGroups.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class ProjectGroupsClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            public Add(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_AddTheProjectGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                ProjectGroupDetails? projectGroup = null;

                try
                {
                    projectGroup = await sut.Add(await domain(), await projectName(), new AddProjectGroup(new PermissionSet { Id = await permissionSetId() }) { GroupId = await groupId() });

                    projectGroup.Should().NotBeNull();
                }
                finally
                {
                    if (projectGroup is not null)
                    {
                        await sut.Remove(await domain(), await projectName(), projectGroup.Id);
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
            public async Task Should_ReturnTheProjectGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new ProjectGroupExistsPrecondition(Fixture.BuddyClient.ProjectGroups, domain, projectName, permissionSetId, groupId), out var projectGroupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var projectGroup = await sut.Get(await domain(), await projectName(), await projectGroupId());

                projectGroup.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await groupId()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheProjectGroups()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new ProjectGroupExistsPrecondition(Fixture.BuddyClient.ProjectGroups, domain, projectName, permissionSetId, groupId))
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var projectGroups = await sut.List(await domain(), await projectName());

                projectGroups?.Groups.Should().NotBeEmpty();
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_RemoveTheProjectGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new ProjectGroupExistsPrecondition(Fixture.BuddyClient.ProjectGroups, domain, projectName, permissionSetId, groupId), out var projectGroupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                await sut.Remove(await domain(), await projectName(), await projectGroupId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await projectGroupId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var act = FluentActions.Awaiting(async () => await sut.Remove(await domain(), await projectName(), await groupId()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheProjectGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var currentPermissionSetId)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var newPermissionSetId)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new ProjectGroupExistsPrecondition(Fixture.BuddyClient.ProjectGroups, domain, projectName, currentPermissionSetId, groupId), out var projectGroupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var projectGroup = await sut.Update(await domain(), await projectName(), await projectGroupId(), new UpdateProjectGroup(new PermissionSet { Id = await newPermissionSetId() }));

                projectGroup?.PermissionSet?.Id.Should().BeEquivalentTo(await newPermissionSetId());
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain), out var projectName)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectGroups;

                var act = FluentActions.Awaiting(async () => await sut.Remove(await domain(), await projectName(), await groupId()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}