namespace BuddyApiClient.IntegrationTest.Workspaces
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;

    public sealed class WorkspacesClientTest
    {
        public sealed class Get : BuddyClientTest
        {
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheWorkspace()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Workspaces;

                var workspace = await sut.Get(await domain());

                workspace.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheDomainDoesNotExist()
            {
                var sut = Fixture.BuddyClient.Workspaces;

                var act = FluentActions.Awaiting(async () => await sut.Get(DomainFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheWorkspaces()
            {
                var sut = Fixture.BuddyClient.Workspaces;

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().NotBeEmpty();
            }
        }
    }
}