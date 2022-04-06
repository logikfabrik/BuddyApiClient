namespace BuddyApiClient.Test
{
    using System;
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
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public sealed class ServiceCollectionExtensionsTest
    {
        public sealed class AddBuddyClient
        {
            [Fact]
            public void Should_Return_BuddyClient_When_Added_Using_Configuration_Parameter()
            {
                var configuration = new ConfigurationBuilder()
                    .Build();

                var sut = new ServiceCollection()
                    .AddBuddyClient(configuration)
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_BuddyClient_When_Added_Using_Action_Parameter()
            {
                var sut = new ServiceCollection()
                    .AddBuddyClient(_ => { })
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_BuddyClient_When_Added_Using_Options_Parameter()
            {
                var sut = new ServiceCollection()
                    .AddBuddyClient(new BuddyClientOptions())
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }
        }

        public sealed class AddBuddyClients
        {
            private readonly IServiceProvider _sut;

            public AddBuddyClients()
            {
                _sut = new ServiceCollection()
                    .AddBuddyClients()
                    .BuildServiceProvider();
            }

            [Fact]
            public void Should_Return_CurrentUserClient()
            {
                var currentUserClient = _sut.GetService<ICurrentUserClient>();

                currentUserClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_CurrentUserEmailsClient()
            {
                var currentUserEmailsClient = _sut.GetService<ICurrentUserEmailsClient>();

                currentUserEmailsClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_GroupMembersClient()
            {
                var groupMembersClient = _sut.GetService<IGroupMembersClient>();

                groupMembersClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_GroupsClient()
            {
                var groupsClient = _sut.GetService<IGroupsClient>();

                groupsClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_MembersClient()
            {
                var membersClient = _sut.GetService<IMembersClient>();

                membersClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_PermissionSetsClient()
            {
                var permissionSetsClient = _sut.GetService<IPermissionSetsClient>();

                permissionSetsClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_ProjectGroupsClient()
            {
                var projectGroupsClient = _sut.GetService<IProjectGroupsClient>();

                projectGroupsClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_ProjectMembersClient()
            {
                var projectMembersClient = _sut.GetService<IProjectMembersClient>();

                projectMembersClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_ProjectsClient()
            {
                var projectsClient = _sut.GetService<IProjectsClient>();

                projectsClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_VariablesClient()
            {
                var variablesClient = _sut.GetService<IVariablesClient>();

                variablesClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_WorkspacesClient()
            {
                var workspacesClient = _sut.GetService<IWorkspacesClient>();

                workspacesClient.Should().NotBeNull();
            }
        }
    }
}