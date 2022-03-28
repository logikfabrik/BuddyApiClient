namespace BuddyApiClient.IntegrationTest.PermissionSets
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus;
    using BuddyApiClient.PermissionSets.Models.Request;
    using FluentAssertions;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            private readonly Faker _faker;
            private readonly Preconditions _preconditions;

            public Create(BuddyClientFixture fixture) : base(fixture)
            {
                _faker = new Faker();
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Create_And_Return_The_PermissionSet()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out _, out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Create(await domain(), new CreatePermissionSet(_faker.Lorem.Word()) { Description = _faker.Lorem.Slug() });

                permissionSet.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _preconditions.TearDown();
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
            public async Task Should_Return_The_PermissionSet_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domainPrecondition, out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domainPrecondition, new Faker().Lorem.Word()), out _, out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Get(await domain(), await permissionSetId());

                permissionSet.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _preconditions.TearDown();
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
            public async Task Should_Return_PermissionSets_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domainPrecondition, out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domainPrecondition, new Faker().Lorem.Word()))
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSets = await sut.List(await domain());

                permissionSets?.PermissionSets.Should().NotBeEmpty();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _preconditions.TearDown();
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
            public async Task Should_Delete_The_PermissionSet_And_Return_Nothing()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domainPrecondition, out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domainPrecondition, new Faker().Lorem.Word()), out _, out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                await sut.Delete(await domain(), await permissionSetId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await permissionSetId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _preconditions.TearDown();
            }
        }

        public sealed class Update : BuddyClientTest
        {
            private readonly Faker _faker;
            private readonly Preconditions _preconditions;

            public Update(BuddyClientFixture fixture) : base(fixture)
            {
                _faker = new Faker();
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Permission_Set()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domainPrecondition, out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domainPrecondition, _faker.Lorem.Word()), out _, out var permissionSetId)
                    .SetUp();

                var name = _faker.Lorem.Word();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Update(await domain(), await permissionSetId(), new UpdatePermissionSet { Name = name });

                permissionSet?.Name.Should().Be(name);
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _preconditions.TearDown();
            }
        }
    }
}