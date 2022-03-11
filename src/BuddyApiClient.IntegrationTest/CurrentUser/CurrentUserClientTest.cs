namespace BuddyApiClient.IntegrationTest.CurrentUser
{
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using BuddyApiClient.CurrentUser.Models.Request;
    using Shouldly;
    using Xunit;

    [Collection(nameof(BuddyClientCollection))]
    public sealed class CurrentUserClientTest
    {
        private readonly BuddyClientFixture _fixture;

        public CurrentUserClientTest(BuddyClientFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get_Should_Return_The_Current_User()
        {
            var sut = _fixture.BuddyClient.CurrentUser;

            var currentUser = await sut.Get();

            currentUser.ShouldNotBeNull();
        }

        [Theory]
        [AutoData]
        public async Task Update_Should_Update_And_Return_The_Current_User(string name)
        {
            var sut = _fixture.BuddyClient.CurrentUser;

            var currentUser = await sut.Update(new UpdateUser { Name = name });

            currentUser?.Name.ShouldBe(name);
        }
    }
}