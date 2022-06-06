namespace BuddyApiClient.Test.PermissionSets
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.PermissionSets.Models.Request;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class PermissionSetsClientTest
    {
        private static IPermissionSetsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new PermissionSetsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Create_Should_CreateThePermissionSet.json")]
            public async Task Should_CreateThePermissionSet(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Create(new Domain("buddy"), new CreatePermissionSet("Artist") { Description = "Artists can access view source" });

                permissionSet.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Get_Should_ReturnThePermissionSet.json")]
            public async Task Should_ReturnThePermissionSet(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Get(new Domain("buddy"), new PermissionSetId(3));

                permissionSet.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_ThePermissionSetDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new PermissionSetId(3)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/List_Should_ReturnThePermissionSets.json")]
            public async Task Should_ReturnThePermissionSets(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSets = await sut.List(new Domain("buddy"));

                permissionSets?.PermissionSets.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"PermissionSets/.testdata/List_Should_ReturnNoPermissionSets_When_NoneExist.json")]
            public async Task Should_ReturnNoPermissionSets_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/permissions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSets = await sut.List(new Domain("buddy"));

                permissionSets?.PermissionSets.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_DeleteThePermissionSet()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete(new Domain("buddy"), new PermissionSetId(3));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"PermissionSets/.testdata/Update_Should_UpdateThePermissionSet.json")]
            public async Task Should_UpdateThePermissionSet(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/permissions/3").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var permissionSet = await sut.Update(new Domain("buddy"), new PermissionSetId(3), new UpdatePermissionSet());

                permissionSet.Should().NotBeNull();
            }
        }
    }
}