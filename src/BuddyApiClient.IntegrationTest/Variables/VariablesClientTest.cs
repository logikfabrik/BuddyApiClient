namespace BuddyApiClient.IntegrationTest.Variables
{
    using System.Net;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Variables.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Variables.Preconditions;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Variables.Models.Response;

    public sealed class VariablesClientTest
    {
        public sealed class Create : BuddyClientTest
        {
            public Create(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_CreateTheVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                VariableDetails? variable = null;

                try
                {
                    variable = await sut.Create(await domain(), CreateVariableRequestFactory.Create());

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
            public async Task Should_CreateTheSshKeyVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                VariableDetails? variable = null;

                try
                {
                    variable = await sut.Create(await domain(), CreateSshKeyVariableRequestFactory.Create());

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
            public async Task Should_ReturnTheVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new VariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var variable = await sut.Get(await domain(), await variableId());

                variable.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_ReturnTheSshKeyVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new SshKeyVariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var variable = await sut.Get(await domain(), await variableId());

                variable.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheVariableDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), VariableIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheVariables()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new VariableExistsPrecondition(Fixture.BuddyClient.Variables, domain))
                    .Add(new SshKeyVariableExistsPrecondition(Fixture.BuddyClient.Variables, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var variables = await sut.List(await domain());

                variables?.Variables.Should().NotBeEmpty();
            }
        }

        public sealed class Delete : BuddyClientTest
        {
            public Delete(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_DeleteTheVariable()
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

            [Fact]
            public async Task Should_Throw_When_TheVariableDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var act = FluentActions.Awaiting(async () => await sut.Delete(await domain(), VariableIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new VariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var model = UpdateVariableRequestFactory.Create();

                var variable = await sut.Update(await domain(), await variableId(), model);

                variable?.Description.Should().Be(model.Description);
            }

            [Fact]
            public async Task Should_UpdateTheSshKeyVariable()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new SshKeyVariableExistsPrecondition(Fixture.BuddyClient.Variables, domain), out var variableId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var model = UpdateSshKeyVariableRequestFactory.Create();

                var variable = await sut.Update(await domain(), await variableId(), model);

                variable?.Description.Should().Be(model.Description);
            }

            [Fact]
            public async Task Should_Throw_When_TheVariableDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Variables;

                var model = UpdateVariableRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), VariableIdFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}