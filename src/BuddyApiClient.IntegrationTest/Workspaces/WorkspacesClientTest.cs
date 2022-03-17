namespace BuddyApiClient.IntegrationTest.Workspaces
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Xunit;

    [Collection(nameof(BuddyClientCollection))]
    public sealed class WorkspacesClientTest
    {
        private const string Domain = "logikfabrik";

        private readonly BuddyClientFixture _fixture;

        public WorkspacesClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_For_Workspace_That_Exists_Should_Return_The_Workspace()
        {
            var sut = _fixture.BuddyClient.Workspaces;

            var workspace = await sut.Get(Domain);

            workspace.Should().NotBeNull();
        }

        [Fact]
        public async Task List_Should_Return_The_Workspaces()
        {
            var sut = _fixture.BuddyClient.Workspaces;

            var workspaces = await sut.List();

            workspaces.Should().NotBeNull();
        }
    }
}