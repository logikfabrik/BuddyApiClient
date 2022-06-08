namespace BuddyApiClient.Test.CurrentUser
{
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUser.Models.Request;
    using BuddyApiClient.Test.Testing;
    using RichardSzalay.MockHttp;

    public sealed class CurrentUserClientTest
    {
        private static ICurrentUserClient CreateClient(MockHttpMessageHandler handler)
        {
            return new CurrentUserClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"CurrentUser/.testdata/Get_Should_ReturnTheCurrentUser.json")]
            public async Task Should_ReturnTheCurrentUser(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/user").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var currentUser = await sut.Get();

                currentUser.Should().NotBeNull();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"CurrentUser/.testdata/Update_Should_UpdateTheCurrentUser.json")]
            public async Task Should_UpdateTheCurrentUser(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/user").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var currentUser = await sut.Update(new UpdateUser { Name = "Mike Benson" });

                currentUser.Should().NotBeNull();
            }
        }
    }
}