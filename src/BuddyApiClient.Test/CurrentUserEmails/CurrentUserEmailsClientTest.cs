namespace BuddyApiClient.Test.CurrentUserEmails
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using BuddyApiClient.Test.Testing;
    using RichardSzalay.MockHttp;

    public sealed class CurrentUserEmailsClientTest
    {
        private static ICurrentUserEmailsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new CurrentUserEmailsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Add
        {
            [Theory]
            [FileTextData(@"CurrentUserEmails/.testdata/Add_Should_AddTheEmail.json")]
            public async Task Should_AddTheEmail(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/user/emails").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var email = await sut.Add(new AddEmail("mike.benson@buddy.works"));

                email.Should().NotBeNull();
            }
        }

        public sealed class List
        {
            [Theory]
            [FileTextData(@"CurrentUserEmails/.testdata/List_Should_ReturnTheEmails.json")]
            public async Task Should_ReturnTheEmails(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/user/emails").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var emails = await sut.List();

                emails?.Emails.Should().NotBeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_RemoveTheEmail()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/user/emails/mike.benson@buddy.works").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove("mike.benson@buddy.works");

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }
    }
}