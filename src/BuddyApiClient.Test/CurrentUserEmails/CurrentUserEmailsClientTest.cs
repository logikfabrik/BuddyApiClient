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
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class CurrentUserEmailsClientTest
    {
        [Theory]
        [FileData(@".\CurrentUserEmails\.testdata\Add_Should_Add_And_Return_The_Added_Email.json")]
        public async Task Add_Should_Add_And_Return_The_Added_Email(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Post, "https://api.buddy.works/user/emails").Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var email = await sut.Add(new AddEmail("mike.benson@buddy.works"));

            email.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@".\CurrentUserEmails\.testdata\List_Should_Return_The_Emails.json")]
        public async Task List_Should_Return_The_Emails(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Get, "https://api.buddy.works/user/emails").Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var emails = await sut.List();

            emails.ShouldNotBeNull();
        }

        [Fact]
        public async Task Remove_Should_Remove_The_Email_And_Return_Nothing()
        {
            var handlerStub = new MockHttpMessageHandler();

            handlerStub.When(HttpMethod.Delete, "https://api.buddy.works/user/emails/mike.benson@buddy.works").Respond(HttpStatusCode.NoContent);

            var sut = CreateClient(handlerStub);

            await sut.Remove("mike.benson@buddy.works");
        }

        private static ICurrentUserEmailsClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new CurrentUserEmailsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri("https://api.buddy.works"), "PAT")));
        }
    }
}