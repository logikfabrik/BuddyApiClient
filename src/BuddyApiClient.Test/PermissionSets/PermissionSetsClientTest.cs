namespace BuddyApiClient.Test.PermissionSets
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.PermissionSets.Models.Request;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        private const string BaseUrl = "https://api.buddy.works";
        private const string Domain = "buddy";
        private const int PermissionSetId = 3;

        [Theory]
        [FileData(@"PermissionSets/.testdata/Create_Should_Create_And_Return_The_Created_Permission_Set.json")]
        public async Task Create_Should_Create_And_Return_The_Created_Permission_Set(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions").ToString();

            handlerStub.When(HttpMethod.Post, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var permissionSet = await sut.Create(Domain, new CreatePermissionSet("Artist") { Description = "Artists can access view source" });

            permissionSet.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"PermissionSets/.testdata/Get_For_Permission_Set_That_Exists_Should_Return_The_Permission_Set.json")]
        public async Task Get_For_Permission_Set_That_Exists_Should_Return_The_Permission_Set(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions/{PermissionSetId}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var permissionSet = await sut.Get(Domain, PermissionSetId);

            permissionSet.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_For_Permission_Set_That_Does_Not_Exist_Should_Throw()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions/{PermissionSetId}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(HttpStatusCode.NotFound);

            var sut = CreateClient(handlerStub);

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, PermissionSetId));

            e.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"PermissionSets/.testdata/List_Should_Return_The_Permission_Sets.json")]
        public async Task List_Should_Return_The_Permission_Sets(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var permissionSets = await sut.List(Domain);

            permissionSets.ShouldNotBeNull();
        }

        [Fact]
        public async Task Delete_Should_Delete_The_Permission_Set_And_Return_Nothing()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions/{PermissionSetId}").ToString();

            handlerStub.When(HttpMethod.Delete, url).Respond(HttpStatusCode.NoContent);

            var sut = CreateClient(handlerStub);

            await sut.Delete(Domain, PermissionSetId);
        }

        [Theory]
        [FileData(@"PermissionSets/.testdata/Update_Should_Update_And_Return_The_Permission_Set.json")]
        public async Task Update_Should_Update_And_Return_The_Permission_Set(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/permissions/{PermissionSetId}").ToString();

            handlerStub.When(HttpMethod.Patch, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var permissionSet = await sut.Update(Domain, PermissionSetId, new UpdatePermissionSet());

            permissionSet.ShouldNotBeNull();
        }

        private static IPermissionSetsClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new PermissionSetsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), null)));
        }
    }
}