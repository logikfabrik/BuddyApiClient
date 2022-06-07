namespace BuddyApiClient.IntegrationTest.GroupMembers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.IntegrationTest.GroupMembers.Preconditions;
    using BuddyApiClient.IntegrationTest.Groups.Preconditions;
    using BuddyApiClient.IntegrationTest.Members.Preconditions;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Workspaces.Preconditions;
    using BuddyApiClient.Members.Models.Response;
    using FluentAssertions;
    using Xunit;

    public sealed class GroupMembersClientTest
    {
        public sealed class Add : BuddyClientTest
        {
            public Add(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_AddTheGroupMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                MemberDetails? groupMember = null;

                try
                {
                    groupMember = await sut.Add(await domain(), await groupId(), new AddGroupMember { MemberId = await memberId() });

                    groupMember.Should().NotBeNull();
                }
                finally
                {
                    if (groupMember is not null)
                    {
                        await sut.Remove(await domain(), await groupId(), groupMember.Id);
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
            public async Task Should_ReturnTheGroupMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .Add(new GroupMemberExistsPrecondition(Fixture.BuddyClient.GroupMembers, domain, groupId, memberId), out var groupMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var groupMember = await sut.Get(await domain(), await groupId(), await groupMemberId());

                groupMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupMemberDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId(), await memberId()));

                // The Buddy API is inconsistent, as it'll return HttpStatusCode.Forbidden, and not HttpStatusCode.NotFound as expected.
                await act.Should().ThrowAsync<HttpRequestException>();
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_ReturnTheGroupMembers()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .Add(new GroupMemberExistsPrecondition(Fixture.BuddyClient.GroupMembers, domain, groupId, memberId))
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var groupMembers = await sut.List(await domain(), await groupId());

                groupMembers?.Members.Should().NotBeEmpty();
            }
        }

        public sealed class Remove : BuddyClientTest
        {
            public Remove(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_RemoveTheGroupMember()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .Add(new GroupMemberExistsPrecondition(Fixture.BuddyClient.GroupMembers, domain, groupId, memberId), out var groupMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                await sut.Remove(await domain(), await groupId(), await groupMemberId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId(), await groupMemberId()));

                // The Buddy API is inconsistent, as it'll return HttpStatusCode.Forbidden, and not HttpStatusCode.NotFound as expected.
                await assert.Should().ThrowAsync<HttpRequestException>();
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupMemberDoesNotExist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var act = FluentActions.Awaiting(async () => await sut.Remove(await domain(), await groupId(), await memberId()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }
    }
}