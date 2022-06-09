namespace BuddyApiClient.Test.Groups
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Request;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class GroupsClientTest
    {
        private static IGroupsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new GroupsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(new Uri("https://api.buddy.works"), string.Empty, handler.ToHttpClient())));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Groups/.testdata/Create_Should_CreateTheGroup.json")]
            public async Task Should_CreateTheGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var group = await sut.Create(new Domain("buddy"), new CreateGroup("JAVA") { Description = "Java developers" });

                group.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Groups/.testdata/Get_Should_ReturnTheGroup.json")]
            public async Task Should_ReturnTheGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var group = await sut.Get(new Domain("buddy"), new GroupId(1));

                group.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheGroupDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new GroupId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Groups/.testdata/List_Should_ReturnTheGroups.json")]
            public async Task Should_ReturnTheGroups(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groups = await sut.List(new Domain("buddy"));

                groups?.Groups.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Groups/.testdata/List_Should_ReturnNoGroups_When_NoneExist.json")]
            public async Task Should_ReturnNoGroups_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groups = await sut.List(new Domain("buddy"));

                groups?.Groups.Should().BeEmpty();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"Groups/.testdata/Update_Should_UpdateTheGroup.json")]
            public async Task Should_UpdateTheGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/groups/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var project = await sut.Update(new Domain("buddy"), new GroupId(1), new UpdateGroup { Description = "Developers" });

                project.Should().NotBeNull();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_DeleteTheGroup()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/groups/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete(new Domain("buddy"), new GroupId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }
    }
}