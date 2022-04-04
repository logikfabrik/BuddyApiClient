namespace BuddyApiClient.Test.Core
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Test.Testing;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class HttpClientFacadeTest
    {
        private static Task<HttpResponseMessage> EmptyJson()
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(string.Empty) });
        }

        public sealed class Get
        {
            [Fact]
            public async Task Should_Make_A_HTTP_GET_Request()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Get, "https://api.buddy.works").Respond(EmptyJson);

                var sut = new HttpClientFacade(handlerMock.ToHttpClient());

                await sut.Get<object>("https://api.buddy.works");

                handlerMock.VerifyNoOutstandingExpectation();
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Client_Error.json")]
            public async Task Should_Throw_On_Client_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Get<object>("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Rate_Error.json")]
            public async Task Should_Throw_On_Rate_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works").Respond(HttpStatusCode.Forbidden, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Get<object>("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.Forbidden, Message = "Rate limit exceeded." });
            }
        }

        public sealed class Post
        {
            [Fact]
            public async Task Should_Make_A_HTTP_POST_Request()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Post, "https://api.buddy.works").Respond(EmptyJson);

                var sut = new HttpClientFacade(handlerMock.ToHttpClient());

                await sut.Post<object>("https://api.buddy.works", new object());

                handlerMock.VerifyNoOutstandingExpectation();
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Client_Error.json")]
            public async Task Should_Throw_On_Client_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Post<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Rate_Error.json")]
            public async Task Should_Throw_On_Rate_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works").Respond(HttpStatusCode.Forbidden, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Post<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.Forbidden, Message = "Rate limit exceeded." });
            }
        }

        public sealed class Patch
        {
            [Fact]
            public async Task Should_Make_A_HTTP_PATCH_Request()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Patch, "https://api.buddy.works").Respond(EmptyJson);

                var sut = new HttpClientFacade(handlerMock.ToHttpClient());

                await sut.Patch<object>("https://api.buddy.works", new object());

                handlerMock.VerifyNoOutstandingExpectation();
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Client_Error.json")]
            public async Task Should_Throw_On_Client_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Patch<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Rate_Error.json")]
            public async Task Should_Throw_On_Rate_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works").Respond(HttpStatusCode.Forbidden, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Patch<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.Forbidden, Message = "Rate limit exceeded." });
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_Make_A_HTTP_DELETE_Request()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works").Respond(HttpStatusCode.NoContent);

                var sut = new HttpClientFacade(handlerMock.ToHttpClient());

                await sut.Delete("https://api.buddy.works");

                handlerMock.VerifyNoOutstandingExpectation();
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Client_Error.json")]
            public async Task Should_Throw_On_Client_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Delete, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Delete("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_On_Rate_Error.json")]
            public async Task Should_Throw_On_Rate_Error(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Delete, "https://api.buddy.works").Respond(HttpStatusCode.Forbidden, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Delete("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.Forbidden, Message = "Rate limit exceeded." });
            }
        }
    }
}