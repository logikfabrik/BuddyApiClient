namespace BuddyApiClient.IntegrationTest.Workspaces
{
    using System.Threading.Tasks;
    using Shouldly;
    using Xunit;

    [Collection(nameof(BuddyClientCollection))]
    public sealed class WorkspacesClientTest
    {
        private readonly BuddyClientFixture _fixture;

        public WorkspacesClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_For_Workspace_That_Exists_Should_Return_The_Workspace()
        {
            var sut = _fixture.BuddyClient.Workspaces;

            var workspace = await sut.Get("logikfabrik");

            workspace.ShouldNotBeNull();
        }

        [Fact]
        public async Task List_Should_Return_The_Workspaces()
        {
            var sut = _fixture.BuddyClient.Workspaces;

            var workspaces = await sut.List();

            workspaces.ShouldNotBeNull();
        }
    }
}