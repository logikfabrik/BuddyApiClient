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

        public sealed class CurrentUser
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.CurrentUser.Should().NotBeNull();
            }
        }

        public sealed class CurrentUserEmails
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.CurrentUserEmails.Should().NotBeNull();
            }
        }

        public sealed class GroupMembers
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.GroupMembers.Should().NotBeNull();
            }
        }

        public sealed class Groups
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.Groups.Should().NotBeNull();
            }
        }

        public sealed class Members
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.Members.Should().NotBeNull();
            }
        }

        public sealed class PermissionSets
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.PermissionSets.Should().NotBeNull();
            }
        }

        public sealed class ProjectGroups
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.ProjectGroups.Should().NotBeNull();
            }
        }

        public sealed class ProjectMembers
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.ProjectMembers.Should().NotBeNull();
            }
        }

        public sealed class Projects
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.Projects.Should().NotBeNull();
            }
        }

        public sealed class Variables
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.Variables.Should().NotBeNull();
            }
        }

        public sealed class Workspaces
        {
            [Fact]
            public void Should_Return_A_Client()
            {
                var sut = CreateClient();

                sut.Workspaces.Should().NotBeNull();
            }
        }
    }
}