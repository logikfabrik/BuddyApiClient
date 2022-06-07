namespace BuddyApiClient.Test
{
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Workspaces;
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
            public void Should_ReturnAnICurrentUserClientInstance()
            {
                var sut = CreateClient();

                sut.CurrentUser.Should().BeAssignableTo<ICurrentUserClient>();
            }
        }

        public sealed class CurrentUserEmails
        {
            [Fact]
            public void Should_ReturnAnICurrentUserEmailsClientInstance()
            {
                var sut = CreateClient();

                sut.CurrentUserEmails.Should().BeAssignableTo<ICurrentUserEmailsClient>();
            }
        }

        public sealed class GroupMembers
        {
            [Fact]
            public void Should_ReturnAnIGroupMembersClientInstance()
            {
                var sut = CreateClient();

                sut.GroupMembers.Should().BeAssignableTo<IGroupMembersClient>();
            }
        }

        public sealed class Groups
        {
            [Fact]
            public void Should_ReturnAnIGroupsClientInstance()
            {
                var sut = CreateClient();

                sut.Groups.Should().BeAssignableTo<IGroupsClient>();
            }
        }

        public sealed class Members
        {
            [Fact]
            public void Should_ReturnAnIMembersClientInstance()
            {
                var sut = CreateClient();

                sut.Members.Should().BeAssignableTo<IMembersClient>();
            }
        }

        public sealed class PermissionSets
        {
            [Fact]
            public void Should_ReturnAnIPermissionSetsClientInstance()
            {
                var sut = CreateClient();

                sut.PermissionSets.Should().BeAssignableTo<IPermissionSetsClient>();
            }
        }

        public sealed class ProjectGroups
        {
            [Fact]
            public void Should_ReturnAnIProjectGroupsClientInstance()
            {
                var sut = CreateClient();

                sut.ProjectGroups.Should().BeAssignableTo<IProjectGroupsClient>();
            }
        }

        public sealed class ProjectMembers
        {
            [Fact]
            public void Should_ReturnAnIProjectMembersClientInstance()
            {
                var sut = CreateClient();

                sut.ProjectMembers.Should().BeAssignableTo<IProjectMembersClient>();
            }
        }

        public sealed class Projects
        {
            [Fact]
            public void Should_ReturnAnIProjectsClientInstance()
            {
                var sut = CreateClient();

                sut.Projects.Should().BeAssignableTo<IProjectsClient>();
            }
        }

        public sealed class Variables
        {
            [Fact]
            public void Should_ReturnAnIVariablesClientInstance()
            {
                var sut = CreateClient();

                sut.Variables.Should().BeAssignableTo<IVariablesClient>();
            }
        }

        public sealed class Workspaces
        {
            [Fact]
            public void Should_ReturnAnIWorkspacesClientInstance()
            {
                var sut = CreateClient();

                sut.Workspaces.Should().BeAssignableTo<IWorkspacesClient>();
            }
        }
    }
}