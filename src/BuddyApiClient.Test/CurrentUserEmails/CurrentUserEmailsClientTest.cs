namespace BuddyApiClient.Test.CurrentUserEmails
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.CurrentUserEmails.Models.Request;
    using BuddyApiClient.Test.Testing;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class CurrentUserEmailsClientTest
    {
        private static ICurrentUserEmailsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new CurrentUserEmailsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"CurrentUserEmails/.testdata/Add_Should_AddTheEmail.json")]
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
            [FileData(@"CurrentUserEmails/.testdata/List_Should_ReturnTheEmails.json")]
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