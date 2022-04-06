namespace BuddyApiClient.IntegrationTest.Variables
{
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
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
    }
}