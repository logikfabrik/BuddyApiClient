namespace BuddyApiClient.IntegrationTest.PermissionSets
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.PermissionSets.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Create(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Create_And_Return_The_PermissionSet()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                PermissionSetDetails? permissionSet = null;

                try
                {
                    permissionSet = await sut.Create(await domain(), new CreatePermissionSet(new Lorem().Word()) { Description = new Lorem().Slug() });

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
            public async Task Should_Return_The_PermissionSet_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Get(await domain(), await permissionSetId());

                permissionSet.Should().NotBeNull();
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
            public async Task Should_Return_PermissionSets_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()))
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSets = await sut.List(await domain());

                permissionSets?.PermissionSets.Should().NotBeEmpty();
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
            public async Task Should_Delete_The_PermissionSet_And_Return_Nothing()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .SetUp();

                var sut = Fixture.BuddyClient.PermissionSets;

                await sut.Delete(await domain(), await permissionSetId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await permissionSetId()));

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
            public async Task Should_Update_And_Return_The_PermissionSet()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new PermissionSetExistsPrecondition(Fixture.BuddyClient.PermissionSets, domain, new Lorem().Word()), out var permissionSetId)
                    .SetUp();

                var name = new Lorem().Word();

                var sut = Fixture.BuddyClient.PermissionSets;

                var permissionSet = await sut.Update(await domain(), await permissionSetId(), new UpdatePermissionSet { Name = name });

                permissionSet?.Name.Should().Be(name);
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