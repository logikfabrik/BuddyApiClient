namespace BuddyApiClient.IntegrationTest.PermissionSets
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.IntegrationTest.PermissionSets.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.PermissionSets.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Create_And_Return_The_Permission_Set()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                PermissionSetDetails? permissionSet = null;

                try
                {
                    permissionSet = await sut.Create(await domain(), CreatePermissionSetFactory.Create());

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
            public async Task Should_Return_The_Permission_Set_If_It_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Get(await domain(), await permissionSetId());

                permissionSet.Should().NotBeNull();
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Permission_Sets_If_Any_Exists()
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
            public async Task Should_Delete_The_Permission_Set_And_Return_Nothing()
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
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Permission_Set()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain), out var permissionSetId)
                    .SetUp();

                var newName = new Lorem().Word();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Update(await domain(), await permissionSetId(), new UpdatePermissionSet { Name = newName });

                permissionSet?.Name.Should().Be(newName);
            }
        }
    }
}