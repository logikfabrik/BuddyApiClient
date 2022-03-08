namespace BuddyApiClient.Test.Core
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class HttpClientFacadeTest
    {
        private const string Url = "https://api.buddy.works/resource";

        [Fact]
        public async Task Get_Should_Do_A_Get_Request()
        {
            var handlerMock = new MockHttpMessageHandler();

            handlerMock.Expect(HttpMethod.Get, Url).Respond(EmptyJson);

            var sut = new HttpClientFacade(handlerMock.ToHttpClient());

            await sut.Get<object>(Url);

            handlerMock.VerifyNoOutstandingExpectation();
        }

        [Theory]
        [FileData(@"Core/.testdata/Get_Should_Throw_On_Client_Error.json")]
        public async Task Get_Should_Throw_On_Client_Error(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Get, Url).Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

            var sut = new HttpClientFacade(handlerStub.ToHttpClient());

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get<object>(Url));

            e.ShouldNotBeNull();
        }

        [Fact]
        public async Task Post_Should_Do_A_Post_Request()
        {
            var handlerMock = new MockHttpMessageHandler();

            handlerMock.Expect(HttpMethod.Post, Url).Respond(EmptyJson);

            var sut = new HttpClientFacade(handlerMock.ToHttpClient());

            await sut.Post<object>(Url, new object());

            handlerMock.VerifyNoOutstandingExpectation();
        }

        [Theory]
        [FileData(@"Core/.testdata/Post_Should_Throw_On_Client_Error.json")]
        public async Task Post_Should_Throw_On_Client_Error(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Post, Url).Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

            var sut = new HttpClientFacade(handlerStub.ToHttpClient());

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Post<object>(Url, new object()));

            e.ShouldNotBeNull();
        }

        [Fact]
        public async Task Patch_Should_Do_A_Patch_Request()
        {
            var handlerMock = new MockHttpMessageHandler();

            handlerMock.Expect(HttpMethod.Patch, Url).Respond(EmptyJson);

            var sut = new HttpClientFacade(handlerMock.ToHttpClient());

            await sut.Patch<object>(Url, new object());

            handlerMock.VerifyNoOutstandingExpectation();
        }

        [Theory]
        [FileData(@"Core/.testdata/Patch_Should_Throw_On_Client_Error.json")]
        public async Task Patch_Should_Throw_On_Client_Error(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Patch, Url).Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

            var sut = new HttpClientFacade(handlerStub.ToHttpClient());

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Patch<object>(Url, new object()));

            e.ShouldNotBeNull();
        }

        [Fact]
        public async Task Delete_Should_Do_A_Delete_Request()
        {
            var handlerMock = new MockHttpMessageHandler();

            handlerMock.Expect(HttpMethod.Delete, Url).Respond(HttpStatusCode.NoContent);

            var sut = new HttpClientFacade(handlerMock.ToHttpClient());

            await sut.Delete(Url);

            handlerMock.VerifyNoOutstandingExpectation();
        }

        [Theory]
        [FileData(@"Core/.testdata/Delete_Should_Throw_On_Client_Error.json")]
        public async Task Delete_Should_Throw_On_Client_Error(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Delete, Url).Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

            var sut = new HttpClientFacade(handlerStub.ToHttpClient());

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Delete(Url));

            e.ShouldNotBeNull();
        }

        private static Task<HttpResponseMessage> EmptyJson()
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(string.Empty) });
        }
    }
}