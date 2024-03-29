﻿namespace BuddyApiClient.Test.Workspaces
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class WorkspacesClientTest
    {
        private static IWorkspacesClient CreateClient(MockHttpMessageHandler handler)
        {
            return new WorkspacesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Workspaces/.testdata/Get_Should_ReturnTheWorkspace.json")]
            public async Task Should_ReturnTheWorkspace(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var workspace = await sut.Get(new Domain("buddy"));

                workspace.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheWorkspaceDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy")));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Workspaces/.testdata/List_Should_ReturnTheWorkspaces.json")]
            public async Task Should_ReturnTheWorkspaces(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var workspaces = await sut.List();

                workspaces?.Workspaces.Should().NotBeEmpty();
            }
        }
    }
}