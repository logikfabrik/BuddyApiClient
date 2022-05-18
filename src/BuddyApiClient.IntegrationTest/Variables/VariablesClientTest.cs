﻿namespace BuddyApiClient.IntegrationTest.Variables
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Variables.Preconditions;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Variables.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class VariablesClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Create_And_Return_The_Variable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                VariableDetails? variable = null;

                try
                {
                    variable = await sut.Create(await domain(), CreateVariableFactory.Create());

                    variable.Should().NotBeNull();
                }
                finally
                {
                    if (variable is not null)
                    {
                        await sut.Delete(await domain(), variable.Id);
                    }
                }
            }

            [Fact]
            public async Task Should_Create_And_Return_The_SSH_Key_Variable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                VariableDetails? variable = null;

                try
                {
                    variable = await sut.Create(await domain(), CreateSshKeyVariableFactory.Create());

                    variable.Should().NotBeNull();
                }
                finally
                {
                    if (variable is not null)
                    {
                        await sut.Delete(await domain(), variable.Id);
                    }
                }
            }
        }

        public sealed class Get : BuddyClientTest
        {
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_The_Variable_If_It_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new VariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var variable = await sut.Get(await domain(), await variableId());

                variable.Should().NotBeNull();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Delete_The_Project_And_Return_Nothing()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new VariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                await sut.Delete(await domain(), await variableId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await variableId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}