namespace BuddyApiClient.IntegrationTest.Workspaces
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    public sealed class WorkspacesClientTest
    {
        public sealed class Get : BuddyClientTest
        {
            private readonly Precondition<string> _domainExistsPrecondition;

            public Get(BuddyClientFixture fixture) : base(fixture)
            {
                _domainExistsPrecondition = new DomainExistsPrecondition(fixture.BuddyClient);
            }

            [Fact]
            public async Task Should_Return_The_Workspace_If_It_Exists()
            {
                var domain = await _domainExistsPrecondition.Arrange();

                var sut = Fixture.BuddyClient.Workspaces;

                var workspace = await sut.Get(domain);

                workspace.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await _domainExistsPrecondition.DisposeAsync();
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Workspaces()
            {
                var sut = Fixture.BuddyClient.Workspaces;

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().NotBeEmpty();
            }
        }
    }
}