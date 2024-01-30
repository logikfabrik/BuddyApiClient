namespace BuddyApiClient.Test
{
    using BuddyApiClient.Actions;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.Pipelines;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Workspaces;
    using RichardSzalay.MockHttp;

    public sealed class BuddyClientTest
    {
        private static IBuddyClient CreateClient()
        {
            return new BuddyClient(string.Empty, new MockHttpMessageHandler().ToHttpClient());
        }

        public sealed class Actions
        {
            [Fact]
            public void Should_ReturnAnIActionsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Actions;

                client.Should().BeAssignableTo<IActionsClient>();
            }
        }

        public sealed class CurrentUser
        {
            [Fact]
            public void Should_ReturnAnICurrentUserClientInstance()
            {
                var sut = CreateClient();

                var client = sut.CurrentUser;

                client.Should().BeAssignableTo<ICurrentUserClient>();
            }
        }

        public sealed class CurrentUserEmails
        {
            [Fact]
            public void Should_ReturnAnICurrentUserEmailsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.CurrentUserEmails;

                client.Should().BeAssignableTo<ICurrentUserEmailsClient>();
            }
        }

        public sealed class GroupMembers
        {
            [Fact]
            public void Should_ReturnAnIGroupMembersClientInstance()
            {
                var sut = CreateClient();

                var client = sut.GroupMembers;

                client.Should().BeAssignableTo<IGroupMembersClient>();
            }
        }

        public sealed class Groups
        {
            [Fact]
            public void Should_ReturnAnIGroupsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Groups;

                client.Should().BeAssignableTo<IGroupsClient>();
            }
        }

        public sealed class Members
        {
            [Fact]
            public void Should_ReturnAnIMembersClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Members;

                client.Should().BeAssignableTo<IMembersClient>();
            }
        }

        public sealed class PermissionSets
        {
            [Fact]
            public void Should_ReturnAnIPermissionSetsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.PermissionSets;

                client.Should().BeAssignableTo<IPermissionSetsClient>();
            }
        }

        public sealed class Pipelines
        {
            [Fact]
            public void Should_ReturnAnIPipelinesClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Pipelines;

                client.Should().BeAssignableTo<IPipelinesClient>();
            }
        }

        public sealed class ProjectGroups
        {
            [Fact]
            public void Should_ReturnAnIProjectGroupsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.ProjectGroups;

                client.Should().BeAssignableTo<IProjectGroupsClient>();
            }
        }

        public sealed class ProjectMembers
        {
            [Fact]
            public void Should_ReturnAnIProjectMembersClientInstance()
            {
                var sut = CreateClient();

                var client = sut.ProjectMembers;

                client.Should().BeAssignableTo<IProjectMembersClient>();
            }
        }

        public sealed class Projects
        {
            [Fact]
            public void Should_ReturnAnIProjectsClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Projects;

                client.Should().BeAssignableTo<IProjectsClient>();
            }
        }

        public sealed class Variables
        {
            [Fact]
            public void Should_ReturnAnIVariablesClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Variables;

                client.Should().BeAssignableTo<IVariablesClient>();
            }
        }

        public sealed class Workspaces
        {
            [Fact]
            public void Should_ReturnAnIWorkspacesClientInstance()
            {
                var sut = CreateClient();

                var client = sut.Workspaces;

                client.Should().BeAssignableTo<IWorkspacesClient>();
            }
        }
    }
}