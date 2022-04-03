namespace BuddyApiClient.IntegrationTest.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class MembersClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Add(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Add_And_Return_The_Member()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                MemberDetails? member = null;

                try
                {
                    member = await sut.Add(await domain(), new AddMember(new Internet().ExampleEmail()));

                    member.Should().NotBeNull();
                }
                finally
                {
                    if (member is not null)
                    {
                        await sut.Remove(await domain(), member.Id);
                    }
                }
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Get : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Get(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_The_Member_If_It_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var member = await sut.Get(await domain(), await memberId());

                member.Should().NotBeNull();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class List : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public List(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_Members_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()))
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var members = await sut.List(await domain());

                members?.Members.Should().NotBeEmpty();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class ListAll : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public ListAll(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Return_Members_If_Any_Exists()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()))
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var members = new List<MemberSummary>();

                var pageQuery = new ListMembersQuery();

                var pageIterator = sut.ListAll(await domain(), pageQuery, (_, response, _) =>
                {
                    members.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                members.Should().NotBeEmpty();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Remove_The_Member_And_Return_Nothing()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                await sut.Remove(await domain(), await memberId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await memberId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public sealed class Update : BuddyClientTest
        {
            private readonly Preconditions _preconditions;

            public Update(BuddyClientFixture fixture) : base(fixture)
            {
                _preconditions = new Preconditions();
            }

            [Fact]
            public async Task Should_Update_And_Return_The_Member()
            {
                await _preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var member = await sut.Update(await domain(), await memberId(), new UpdateMember { Admin = true });

                member?.Admin.Should().BeTrue();
            }

            public override async Task DisposeAsync()
            {
                await base.DisposeAsync();

                await foreach (var precondition in _preconditions.TearDown())
                {
                    if (precondition is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }
    }
}