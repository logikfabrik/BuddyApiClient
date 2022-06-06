namespace BuddyApiClient.IntegrationTest.Members
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.IntegrationTest.Members.FakeModelFactories;
    using BuddyApiClient.IntegrationTest.Members.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class MembersClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            public Add(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_AddTheMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                MemberDetails? member = null;

                try
                {
                    member = await sut.Add(await domain(), AddMemberRequestFactory.Create());

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
        }

        public sealed class Get : BuddyClientTest
        {
            public Get(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var member = await sut.Get(await domain(), await memberId());

                member.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheMemberDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), MemberIdFactory.Create()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheMembers()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain))
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var members = await sut.List(await domain());

                members?.Members.Should().NotBeEmpty();
            }
        }

        public sealed class ListAll : BuddyClientTest
        {
            public ListAll(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheMembers()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain))
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
        }

        public sealed class Remove : BuddyClientTest
        {
            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_RemoveTheMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                await sut.Remove(await domain(), await memberId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await memberId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }

            [Fact]
            public async Task Should_Throw_When_TheMemberDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var act = FluentActions.Awaiting(async () => await sut.Remove(await domain(), MemberIdFactory.Create()));

                // The Buddy API is inconsistent, as it'll return HttpStatusCode.InternalServerError, and not HttpStatusCode.NotFound as expected.
                await act.Should().ThrowAsync<HttpRequestException>();
            }
        }

        public sealed class Update : BuddyClientTest
        {
            public Update(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_UpdateTheMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var model = UpdateMemberRequestFactory.Create();

                var member = await sut.Update(await domain(), await memberId(), model);

                member?.Admin.Should().Be(model.Admin);
            }

            [Fact]
            public async Task Should_Throw_When_TheMemberDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .SetUp();

                var sut = Fixture.BuddyClient.Members;

                var model = UpdateMemberRequestFactory.Create();

                var act = FluentActions.Awaiting(async () => await sut.Update(await domain(), MemberIdFactory.Create(), model));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}