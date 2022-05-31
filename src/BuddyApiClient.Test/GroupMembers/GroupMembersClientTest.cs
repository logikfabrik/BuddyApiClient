namespace BuddyApiClient.Test.GroupMembers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class GroupMembersClientTest
    {
        private static IGroupMembersClient CreateClient(MockHttpMessageHandler handler)
        {
            return new GroupMembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"GroupMembers/.testdata/Add_Should_Add_And_Return_The_Group_Member.json")]
            public async Task Should_Add_And_Return_The_Group_Member(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/groups/1/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMember = await sut.Add(new Domain("buddy"), new GroupId(1), new AddGroupMember { MemberId = new MemberId(1) });

                groupMember.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"GroupMembers/.testdata/Get_Should_Return_The_Group_Member_If_It_Exists.json")]
            public async Task Should_Return_The_Group_Member_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMember = await sut.Get(new Domain("buddy"), new GroupId(1), new MemberId(1));

                groupMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Group_Member_Does_Not_Exist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new GroupId(1), new MemberId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"GroupMembers/.testdata/List_Should_Return_Group_Members_If_Any_Exists.json")]
            public async Task Should_Return_Group_Members_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMembers = await sut.List(new Domain("buddy"), new GroupId(1));

                groupMembers?.Members.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"GroupMembers/.testdata/List_Should_Not_Return_Group_Members_If_None_Exist.json")]
            public async Task Should_Not_Return_Group_Members_If_None_Exist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMembers = await sut.List(new Domain("buddy"), new GroupId(1));

                groupMembers?.Members.Should().BeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_Remove_The_Group_Member_And_Return_Nothing()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/groups/1/members/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove(new Domain("buddy"), new GroupId(1), new MemberId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }
    }
}