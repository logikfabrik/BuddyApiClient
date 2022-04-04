namespace BuddyApiClient.IntegrationTest.Groups
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.Groups.Models.Request;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using FluentAssertions;
    using Xunit;

    public sealed class GroupsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Create(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Create_And_Return_The_Group()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                GroupDetails? group = null;

                try
                {
                    group = await sut.Create(await domain(), new CreateGroup(new Lorem().Word()));

                    group.Should().NotBeNull();
                }
                finally
                {
                    if (group is not null)
                    {
                        await sut.Delete(await domain(), group.Id);
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
            public async Task Should_Return_The_Group_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var group = await sut.Get(await domain(), await groupId());

                group.Should().NotBeNull();
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
            public async Task Should_Return_Groups_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()))
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var groups = await sut.List(await domain());

                groups?.Groups.Should().NotBeEmpty();
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

        public sealed class Delete : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Delete_The_Group_And_Return_Nothing()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                await sut.Delete(await domain(), await groupId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId()));

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
            public async Task Should_Update_And_Return_The_Group()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .SetUp();

                var newName = new Lorem().Word();

                var sut = Fixture.BuddyClient.Groups;

                var group = await sut.Update(await domain(), await groupId(), new UpdateGroup { Name = newName });

                group?.Name.Should().Be(newName);
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