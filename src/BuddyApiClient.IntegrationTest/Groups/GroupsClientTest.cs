namespace BuddyApiClient.IntegrationTest.Groups
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.Groups.Models.Response;
    using BuddyApiClient.IntegrationTest.Groups.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Groups.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using FluentAssertions;
    using Xunit;

    public sealed class GroupsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateTheGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                GroupDetails? group = null;

                try
                {
                    group = await sut.Create(await domain(), CreateGroupRequestFactory.Create());

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
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var group = await sut.Get(await domain(), await groupId());

                group.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), GroupIdFactory.Create()));

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
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var groups = await sut.List(await domain());

                groups?.Groups.Should().NotBeEmpty();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_DeleteTheGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                await sut.Delete(await domain(), await groupId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var act = FluentActions.Awaiting(async () => await sut.Delete(await domain(), GroupIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheGroup()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var model = UpdateGroupRequestFactory.Create();

                var group = await sut.Update(await domain(), await groupId(), model);

                group?.Name.Should().Be(model.Name);
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Groups;

                var model = UpdateGroupRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), GroupIdFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}