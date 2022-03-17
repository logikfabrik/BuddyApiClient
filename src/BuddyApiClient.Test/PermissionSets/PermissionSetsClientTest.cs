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
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        private static IPermissionSetsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new PermissionSetsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Create_Should_Create_And_Return_The_PermissionSet.json")]
            public async Task Should_Create_And_Return_The_PermissionSet(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Create("buddy", new CreatePermissionSet("Artist") { Description = "Artists can access view source" });

                permissionSet.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Get_Should_Return_The_PermissionSet_If_It_Exists.json")]
            public async Task Should_Return_The_PermissionSet_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Get("buddy", 3);

                permissionSet.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_PermissionSet_Does_Not_Exist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get("buddy", 3));

                await act.Should().ThrowAsync<HttpRequestException>();
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/List_Should_Return_PermissionSets_If_Any_Exists.json")]
            public async Task Should_Return_PermissionSets_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSets = await sut.List("buddy");

                permissionSets?.PermissionSets.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"PermissionSets/.testdata/List_Should_Not_Return_PermissionSets_If_None_Exist.json")]
            public async Task Should_Not_Return_PermissionSets_If_None_Exist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSets = await sut.List("buddy");

                permissionSets?.PermissionSets.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_Delete_The_PermissionSet_And_Return_Nothing()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete("buddy", 3);

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Update_Should_Update_And_Return_The_PermissionSet.json")]
            public async Task Should_Update_And_Return_The_Permission_Set(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Update("buddy", 3, new UpdatePermissionSet());

                permissionSet.Should().NotBeNull();
            }
        }
    }
}