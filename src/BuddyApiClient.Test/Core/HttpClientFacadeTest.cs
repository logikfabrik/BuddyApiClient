namespace BuddyApiClient.Test.Core
{
    using System.Net;
    using System.Net.Http.Json;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Test.Testing;
    using RichardSzalay.MockHttp;

    public sealed class HttpClientFacadeTest
    {
        private static async Task MakeARequest(HttpMethod method, Func<HttpClientFacade, Task> act)
        {
            var handlerMock = new MockHttpMessageHandler();

            static Task<HttpResponseMessage> CreateEmptyJsonResponse()
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = JsonContent.Create(string.Empty) });
            }

            handlerMock.Expect(method, "https://api.buddy.works").Respond(CreateEmptyJsonResponse);

            var sut = new HttpClientFacade(handlerMock.ToHttpClient());

            await act(sut);

            handlerMock.VerifyNoOutstandingExpectation();
        }

        public sealed class Get
        {
            [Fact]
            public async Task Should_MakeARequest()
            {
                await MakeARequest(HttpMethod.Get, sut => sut.Get<object>("https://api.buddy.works"));
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsAClientError.json")]
            public async Task Should_Throw_When_ThereIsAClientError(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Get<object>("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsARateError.json")]
            public async Task Should_Throw_When_ThereIsARateError(string responseJson)
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
            public async Task Should_MakeARequest()
            {
                await MakeARequest(HttpMethod.Post, sut => sut.Post<object>("https://api.buddy.works", new object()));
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsAClientError.json")]
            public async Task Should_Throw_When_ThereIsAClientError(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Post<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsARateError.json")]
            public async Task Should_Throw_When_ThereIsARateError(string responseJson)
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
            public async Task Should_MakeARequest()
            {
                await MakeARequest(HttpMethod.Patch, sut => sut.Patch<object>("https://api.buddy.works", new object()));
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsAClientError.json")]
            public async Task Should_Throw_When_ThereIsAClientError(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Patch<object>("https://api.buddy.works", new object()));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsARateError.json")]
            public async Task Should_Throw_When_ThereIsARateError(string responseJson)
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
            public async Task Should_MakeARequest()
            {
                await MakeARequest(HttpMethod.Delete, sut => sut.Delete("https://api.buddy.works"));
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsAClientError.json")]
            public async Task Should_Throw_When_ThereIsAClientError(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Delete, "https://api.buddy.works").Respond(HttpStatusCode.BadRequest, MediaTypeNames.Application.Json, responseJson);

                var sut = new HttpClientFacade(handlerStub.ToHttpClient());

                var act = FluentActions.Awaiting(() => sut.Delete("https://api.buddy.works"));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.Should().BeEquivalentTo(new { StatusCode = HttpStatusCode.BadRequest, Message = "API is disabled in this workspace." });
            }

            [Theory]
            [FileData(@"Core/.testdata/Should_Throw_When_ThereIsARateError.json")]
            public async Task Should_Throw_When_ThereIsARateError(string responseJson)
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