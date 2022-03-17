namespace BuddyApiClient.Test.Workspaces
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Workspaces;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class WorkspacesClientTest
    {
        private static IWorkspacesClient CreateClient(MockHttpMessageHandler handler)
        {
            return new WorkspacesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Workspaces/.testdata/Get_Should_Return_The_Workspace_If_It_Exists.json")]
            public async Task Should_Return_The_Workspace_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var workspace = await sut.Get("buddy");

                workspace.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Workspace_Does_Not_Exist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get("buddy"));

                await act.Should().ThrowAsync<HttpRequestException>();
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Workspaces/.testdata/List_Should_Return_Workspaces_If_Any_Exists.json")]
            public async Task Should_Return_Workspaces_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Workspaces/.testdata/List_Should_Not_Return_Workspaces_If_None_Exist.json")]
            public async Task Should_Not_Return_Workspaces_If_None_Exist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().BeEmpty();
            }
        }
    }
}