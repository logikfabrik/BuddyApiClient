namespace BuddyApiClient.Test.Groups
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.Groups.Models.Request;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class GroupsClientTest
    {
        private static IGroupsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new GroupsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Groups/.testdata/Create_Should_Create_And_Return_The_Group.json")]
            public async Task Should_Create_And_Return_The_Group(string responseJson)
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
            [FileData(@"Groups/.testdata/Get_Should_Return_The_Group_If_It_Exists.json")]
            public async Task Should_Return_The_Group_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var group = await sut.Get(new Domain("buddy"), new GroupId(1));

                group.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Group_Does_Not_Exist()
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
            [FileData(@"Groups/.testdata/List_Should_Return_Groups_If_Any_Exists.json")]
            public async Task Should_Return_Groups_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var groups = await sut.List(new Domain("buddy"));

                groups?.Groups.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Groups/.testdata/List_Should_Not_Return_Groups_If_None_Exist.json")]
            public async Task Should_Not_Return_Groups_If_None_Exist(string responseJson)
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
            [FileData(@"Groups/.testdata/Update_Should_Update_And_Return_The_Group.json")]
            public async Task Should_Update_And_Return_The_Group(string responseJson)
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
            public async Task Should_Delete_The_Group_And_Return_Nothing()
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