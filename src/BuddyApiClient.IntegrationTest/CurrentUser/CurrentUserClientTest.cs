namespace BuddyApiClient.IntegrationTest.CurrentUser
{
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.CurrentUser.Models.Request;
    using BuddyApiClient.IntegrationTest.Testing;
    using FluentAssertions;
    using Xunit;

    public sealed class CurrentUserClientTest
    {
        public sealed class Get : BuddyClientTest
        {
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_The_Current_User()
            {
                var sut = Fixture.BuddyClient.CurrentUser;

                var currentUser = await sut.Get();

                currentUser.Should().NotBeNull();
            }
        }

        public sealed class Update : BuddyClientTest
        {
            private string? _currentName;

            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            public override async Task InitializeAsync()
            {
                await base.InitializeAsync();

                var client = Fixture.BuddyClient.CurrentUser;

                var currentUser = await client.Get();

                _currentName = currentUser?.Name;
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Current_User()
            {
                var newName = new Name().FullName();

                var sut = Fixture.BuddyClient.CurrentUser;

                var currentUser = await sut.Update(new UpdateUser { Name = newName });

                currentUser?.Name.Should().Be(newName);
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                if (_currentName is null)
                {
                    return;
                }

                var client = Fixture.BuddyClient.CurrentUser;

                await client.Update(new UpdateUser { Name = _currentName });
            }
        }
    }
}