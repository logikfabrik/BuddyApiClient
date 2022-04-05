namespace BuddyApiClient.IntegrationTest.GroupMembers
{
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Bogus.DataSets;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.IntegrationTest.Testing;
    using BuddyApiClient.IntegrationTest.Testing.Preconditions;
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
            public async Task Should_Add_And_Return_The_Group_Member()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
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
            public async Task Should_Return_The_Group_Member_If_It_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new GroupMemberExistsPrecondition(Fixture.BuddyClient.GroupMembers, domain, groupId, memberId), out var groupMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var groupMember = await sut.Get(await domain(), await groupId(), await groupMemberId());

                groupMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Group_Member_Does_Not_Exist()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                var act = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId(), await memberId()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            }
        }

        public sealed class List : BuddyClientTest
        {
            public List(BuddyClientFixture fixture) : base(fixture)
            {
            }

            [Fact]
            public async Task Should_Return_Group_Members_If_Any_Exists()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
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
            public async Task Should_Remove_The_Group_Member_And_Return_Nothing()
            {
                await Preconditions
                    .Add(new DomainExistsPrecondition(Fixture.BuddyClient.Workspaces), out var domain)
                    .Add(new GroupExistsPrecondition(Fixture.BuddyClient.Groups, domain, new Lorem().Word()), out var groupId)
                    .Add(new MemberExistsPrecondition(Fixture.BuddyClient.Members, domain, new Internet().ExampleEmail()), out var memberId)
                    .Add(new GroupMemberExistsPrecondition(Fixture.BuddyClient.GroupMembers, domain, groupId, memberId), out var groupMemberId)
                    .SetUp();

                var sut = Fixture.BuddyClient.GroupMembers;

                await sut.Remove(await domain(), await groupId(), await groupMemberId());

                var assert = FluentActions.Awaiting(async () => await sut.Get(await domain(), await groupId(), await groupMemberId()));

                (await assert.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.Forbidden);
            }
        }
    }
}