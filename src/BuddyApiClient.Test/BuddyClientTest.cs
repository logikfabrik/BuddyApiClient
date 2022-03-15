namespace BuddyApiClient.Test
{
    using Microsoft.Extensions.Options;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class BuddyClientTest
    {
        [Fact]
        public void New_Should_Have_Client_For_Current_User()
        {
            var sut = CreateClient();

            sut.CurrentUser.ShouldNotBeNull();
        }

        [Fact]
        public void New_Should_Have_Client_For_Current_User_Emails()
        {
            var sut = CreateClient();

            sut.CurrentUserEmails.ShouldNotBeNull();
        }

        [Fact]
        public void New_Should_Have_Client_For_Members()
        {
            var sut = CreateClient();

            sut.Members.ShouldNotBeNull();
        }

        [Fact]
        public void New_Should_Have_Client_For_Permission_Sets()
        {
            var sut = CreateClient();

            sut.PermissionSets.ShouldNotBeNull();
        }

        [Fact]
        public void New_Should_Have_Client_For_Projects()
        {
            var sut = CreateClient();

            sut.Projects.ShouldNotBeNull();
        }

        [Fact]
        public void New_Should_Have_Client_For_Workspaces()
        {
            var sut = CreateClient();

            sut.PermissionSets.ShouldNotBeNull();
        }

        private static IBuddyClient CreateClient()
        {
            return new BuddyClient(new MockHttpMessageHandler().ToHttpClient(), new OptionsWrapper<BuddyClientOptions>(new BuddyClientOptions()));
        }
    }
}