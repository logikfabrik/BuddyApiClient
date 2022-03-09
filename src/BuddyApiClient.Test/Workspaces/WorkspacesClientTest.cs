namespace BuddyApiClient.Test.Workspaces
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Workspaces;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class WorkspacesClientTest
    {
        private const string BaseUrl = "https://api.buddy.works";
        private const string Domain = "buddy";

        [Theory]
        [FileData(@"Workspaces/.testdata/Get_For_Workspace_That_Exists_Should_Return_The_Workspace.json")]
        public async Task Get_For_Workspace_That_Exists_Should_Return_The_Workspace(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var workspace = await sut.Get(Domain);

            workspace.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_For_Workspace_That_Does_Not_Exist_Should_Throw()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(HttpStatusCode.NotFound);

            var sut = CreateClient(handlerStub);

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain));

            e.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Workspaces/.testdata/List_Should_Return_The_Workspaces.json")]
        public async Task List_Should_Return_The_Workspaces(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), "workspaces").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var workspaces = await sut.List();

            workspaces.ShouldNotBeNull();
        }

        private static IWorkspacesClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new WorkspacesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), null)));
        }
    }
}