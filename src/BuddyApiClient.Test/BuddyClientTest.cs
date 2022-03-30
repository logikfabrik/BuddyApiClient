namespace BuddyApiClient.Test
{
    using FluentAssertions;
    using Microsoft.Extensions.Options;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class BuddyClientTest
    {
        private static IBuddyClient CreateClient()
        {
            return new BuddyClient(new MockHttpMessageHandler().ToHttpClient(), new OptionsWrapper<BuddyClientOptions>(new BuddyClientOptions()));
        }

        public sealed class Constructor
        {
            [Fact]
            public void Should_Return_Instance_With_CurrentUser_Set()
            {
                var sut = CreateClient();

                sut.CurrentUser.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Instance_With_CurrentUserEmails_Set()
            {
                var sut = CreateClient();

                sut.CurrentUserEmails.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Instance_With_Members_Set()
            {
                var sut = CreateClient();

                sut.Members.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Instance_With_PermissionSets_Set()
            {
                var sut = CreateClient();

                sut.PermissionSets.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Instance_With_Projects_Set()
            {
                var sut = CreateClient();

                sut.Projects.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Instance_With_Workspaces_Set()
            {
                var sut = CreateClient();

                sut.Workspaces.Should().NotBeNull();
            }
        }
    }
}