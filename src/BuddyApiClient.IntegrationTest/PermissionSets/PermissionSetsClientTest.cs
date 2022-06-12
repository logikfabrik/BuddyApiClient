namespace BuddyApiClient.IntegrationTest.PermissionSets
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.PermissionSets.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.PermissionSets.Models.Response;

    public sealed class PermissionSetsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateThePermissionSet()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                PermissionSetDetails? permissionSet = null;

                try
                {
                    permissionSet = await sut.Create(await domain(), CreatePermissionSetRequestFactory.Create());

                    permissionSet.Should().NotBeNull();
                }
                finally
                {
                    if (permissionSet is not null)
                    {
                        await sut.Delete(await domain(), permissionSet.Id);
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
            public async Task Should_ReturnThePermissionSet()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Get(await domain(), await permissionSetId());

                permissionSet.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_ThePermissionSetDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), PermissionSetIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnThePermissionSets()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSets = await sut.List(await domain());

                permissionSets?.PermissionSets.Should().NotBeEmpty();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_DeleteThePermissionSet()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                await sut.Delete(await domain(), await permissionSetId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await permissionSetId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_ThePermissionSetDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var act = FluentActions.Awaiting(async () => await sut.Delete(await domain(), PermissionSetIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateThePermissionSet()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var model = UpdatePermissionSetRequestFactory.Create();

                var permissionSet = await sut.Update(await domain(), await permissionSetId(), model);

                permissionSet?.Name.Should().Be(model.Name);
            }

            [Fact]
            public async Task Should_Throw_When_ThePermissionSetDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var model = UpdatePermissionSetRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), PermissionSetIdFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}