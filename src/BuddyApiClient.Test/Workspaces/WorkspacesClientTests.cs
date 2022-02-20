﻿namespace BuddyApiClient.Test.Workspaces;

using System;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using BuddyApiClient.Core;
using BuddyApiClient.Workspaces;
using RichardSzalay.MockHttp;
using Shouldly;
using Xunit;

public sealed class WorkspacesClientTests
{
    [Theory]
    [FileData(@".\Workspaces\.testdata\Get_For_Workspace_That_Exists_Should_Return_Workspace.json")]
    public async Task Get_For_Workspace_That_Exists_Should_Return_Workspace(string responseJson)
    {
        var handlerStub = new MockHttpMessageHandler();

        handlerStub
            .When("https://api.buddy.works/workspaces/buddy")
            .Respond(MediaTypeNames.Application.Json, responseJson);

        var sut = CreateClient(handlerStub);

        var workspace = await sut.Get("buddy");

        workspace.ShouldNotBeNull();
    }

    [Fact]
    public async Task Get_For_Workspace_That_Does_Not_Exist_Should_Throw()
    {
        var handlerStub = new MockHttpMessageHandler();

        handlerStub
            .When("https://api.buddy.works/workspaces/buddy")
            .Throw(new HttpRequestException());

        var sut = CreateClient(handlerStub);

        var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get("buddy"));

        e.ShouldNotBeNull();
    }

    [Theory]
    [FileData(@".\Workspaces\.testdata\GetList_Should_Return_Workspaces.json")]
    public async Task GetList_Should_Return_Workspaces(string responseJson)
    {
        var handlerStub = new MockHttpMessageHandler();

        handlerStub
            .When("https://api.buddy.works/workspaces")
            .Respond(MediaTypeNames.Application.Json, responseJson);

        var sut = CreateClient(handlerStub);

        var workspaces = await sut.GetList();

        workspaces.ShouldNotBeNull();
    }

    private static IWorkspacesClient CreateClient(MockHttpMessageHandler handlerStub) => new WorkspacesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), "PAT")));
}
