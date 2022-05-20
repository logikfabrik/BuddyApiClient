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

        [Fact]
        public void CurrentUser_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.CurrentUser.Should().NotBeNull();
        }

        [Fact]
        public void CurrentUserEmails_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.CurrentUserEmails.Should().NotBeNull();
        }

        [Fact]
        public void GroupMembers_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.GroupMembers.Should().NotBeNull();
        }

        [Fact]
        public void Groups_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.Groups.Should().NotBeNull();
        }

        [Fact]
        public void Members_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.Members.Should().NotBeNull();
        }

        [Fact]
        public void PermissionSets_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.PermissionSets.Should().NotBeNull();
        }

        [Fact]
        public void ProjectGroups_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.ProjectGroups.Should().NotBeNull();
        }

        [Fact]
        public void ProjectMembers_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.ProjectMembers.Should().NotBeNull();
        }

        [Fact]
        public void Projects_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.Projects.Should().NotBeNull();
        }

        [Fact]
        public void Variables_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.Variables.Should().NotBeNull();
        }

        [Fact]
        public void Workspaces_Should_Return_A_Client()
        {
            var sut = CreateClient();

            sut.Workspaces.Should().NotBeNull();
        }
    }
}