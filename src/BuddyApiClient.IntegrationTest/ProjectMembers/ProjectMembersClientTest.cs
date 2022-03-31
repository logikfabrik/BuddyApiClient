namespace BuddyApiClient.IntegrationTest.ProjectMembers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.ProjectMembers.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class ProjectMembersClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Add(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Add_And_Return_The_Project_Member()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                ProjectMemberDetails? projectMember = null;

                try
                {
                    projectMember = await sut.Add(await domain(), await projectName(), new AddProjectMember(new PermissionSet { Id = await permissionSetId() }) { MemberId = await memberId() });

                    projectMember.Should().NotBeNull();
                }
                finally
                {
                    if (projectMember is not null)
                    {
                        await sut.Remove(await domain(), await projectName(), projectMember.Id);
                    }
                }
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Get : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Get(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_The_Project_Member_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new ProjectMemberExistsPrecondition(Fixture.BuddyClient.ProjectMembers, domain, projectName, permissionSetId, memberId), out var projectMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                var projectMember = await sut.Get(await domain(), await projectName(), await projectMemberId());

                projectMember.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class List : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public List(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_Project_Members_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new ProjectMemberExistsPrecondition(Fixture.BuddyClient.ProjectMembers, domain, projectName, permissionSetId, memberId))
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                var projectMembers = await sut.List(await domain(), await projectName());

                projectMembers?.Members.Should().NotBeEmpty();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class ListAll : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public ListAll(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_Project_Members_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new ProjectMemberExistsPrecondition(Fixture.BuddyClient.ProjectMembers, domain, projectName, permissionSetId, memberId))
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                var projectMembers = new List<MemberSummary>();

                var pageQuery = new ListMembersQuery();

                var pageIterator = sut.ListAll(await domain(), await projectName(), pageQuery, (_, response, _) =>
                {
                    projectMembers.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                projectMembers.Should().NotBeEmpty();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Remove_The_Project_Member_And_Return_Nothing()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new ProjectMemberExistsPrecondition(Fixture.BuddyClient.ProjectMembers, domain, projectName, permissionSetId, memberId), out var projectMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                await sut.Remove(await domain(), await projectName(), await projectMemberId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await projectName(), await projectMemberId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Update : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Update(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Project_Member()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new ProjectExistsPrecondition(Fixture.BuddyClient.Projects, domain, new Lorem().Word()), out var projectName)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var currentPermissionSetId)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var newPermissionSetId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new ProjectMemberExistsPrecondition(Fixture.BuddyClient.ProjectMembers, domain, projectName, currentPermissionSetId, memberId), out var projectMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.ProjectMembers;

                var projectMember = await sut.Update(await domain(), await projectName(), await projectMemberId(), new UpdateProjectMember(new PermissionSet { Id = await newPermissionSetId() }));

                projectMember?.PermissionSet?.Id.Should().BeEquivalentTo(await newPermissionSetId());
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
    }
}