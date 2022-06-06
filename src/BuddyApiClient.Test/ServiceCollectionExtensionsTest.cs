namespace BuddyApiClient.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
    using Microsoft.Extensions.Options;
    using Xunit;

    public sealed class ServiceCollectionExtensionsTest
    {
        private static bool IsDescriptor<T>(ServiceDescriptor descriptor) => typeof(T).IsAssignableFrom(descriptor.ServiceType);

        public sealed class AddBuddyClient
        {
            [Fact]
            public void Should_AddAIConfigureOptionsServiceDescriptor_When_CalledWithNamedConfigurationSection()
            {
                var namedConfigurationSection = new ConfigurationRoot(new List<IConfigurationProvider>());

                var sut = new ServiceCollection().AddBuddyClient(namedConfigurationSection);

                sut.Any(IsDescriptor<IConfigureOptions<BuddyClientOptions>>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIConfigureOptionsServiceDescriptor_When_CalledWithConfigureOptions()
            {
                var configureOptions = new Action<BuddyClientOptions>(options => { });

                var sut = new ServiceCollection().AddBuddyClient(configureOptions);

                sut.Any(IsDescriptor<IConfigureOptions<BuddyClientOptions>>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIConfigureOptionsServiceDescriptor_When_CalledWithUserOptions()
            {
                var userOptions = new BuddyClientOptions();

                var sut = new ServiceCollection().AddBuddyClient(userOptions);

                sut.Any(IsDescriptor<IConfigureOptions<BuddyClientOptions>>).Should().BeTrue();
            }
        }

        public sealed class AddClient
        {
            [Fact]
            public void Should_AddAIBuddyClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddClient();

                sut.Any(IsDescriptor<IBuddyClient>).Should().BeTrue();
            }
        }

        public sealed class AddResourceClients
        {
            [Fact]
            public void Should_AddAICurrentUserClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<ICurrentUserClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAICurrentUserEmailsClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<ICurrentUserEmailsClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIGroupMembersClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IGroupMembersClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIGroupsClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IGroupsClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIMembersClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IMembersClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIPermissionSetsClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IPermissionSetsClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIProjectGroupsClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IProjectGroupsClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIProjectMembersClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IProjectMembersClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIProjectsClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IProjectsClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIVariablesClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IVariablesClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIWorkspacesClientServiceDescriptor()
            {
                var sut = new ServiceCollection();

                sut.AddResourceClients();

                sut.Any(IsDescriptor<IWorkspacesClient>).Should().BeTrue();
            }
        }
    }
}