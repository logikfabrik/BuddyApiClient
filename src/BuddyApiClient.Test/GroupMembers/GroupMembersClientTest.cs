namespace BuddyApiClient.Test.GroupMembers
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.GroupMembers;
    using BuddyApiClient.GroupMembers.Models.Request;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class GroupMembersClientTest
    {
        private static IGroupMembersClient CreateClient(MockHttpMessageHandler handler)
        {
            return new GroupMembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(new Uri("https://api.buddy.works"), string.Empty, handler.ToHttpClient())));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"GroupMembers/.testdata/Add_Should_AddTheGroupMember.json")]
            public async Task Should_AddTheGroupMember(string responseJson)
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
            [FileData(@"GroupMembers/.testdata/Get_Should_ReturnTheGroupMember.json")]
            public async Task Should_ReturnTheGroupMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMember = await sut.Get(new Domain("buddy"), new GroupId(1), new MemberId(1));

                groupMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupMemberDoesNotExist()
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
            [FileData(@"GroupMembers/.testdata/List_Should_ReturnTheGroupMembers.json")]
            public async Task Should_ReturnTheGroupMembers(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groupMembers = await sut.List(new Domain("buddy"), new GroupId(1));

                groupMembers?.Members.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"GroupMembers/.testdata/List_Should_ReturnNoGroupMembers_When_NoneExist.json")]
            public async Task Should_ReturnNoGroupMembers_When_NoneExist(string responseJson)
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
            public async Task Should_RemoveTheGroupMember()
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