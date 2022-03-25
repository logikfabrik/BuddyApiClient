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
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class CurrentUserEmailsClientTest
    {
        private static ICurrentUserEmailsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new CurrentUserEmailsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"CurrentUserEmails/.testdata/Add_Should_Add_And_Return_The_Email.json")]
            public async Task Should_Add_And_Return_The_Email(string responseJson)
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
            [FileData(@"CurrentUserEmails/.testdata/List_Should_Return_Emails.json")]
            public async Task Should_Return_Emails(string responseJson)
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
            public async Task Should_Remove_The_Email_And_Return_Nothing()
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