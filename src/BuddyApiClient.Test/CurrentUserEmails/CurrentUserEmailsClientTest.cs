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
        private const string BaseUrl = "https://api.buddy.works";

        [Theory]
        [FileData(@"CurrentUserEmails/.testdata/Add_Should_Add_And_Return_The_Added_Email.json")]
        public async Task Add_Should_Add_And_Return_The_Added_Email(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), "user/emails").ToString();

            handlerStub.When(HttpMethod.Post, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var email = await sut.Add(new AddEmail("mike.benson@buddy.works"));

            email.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"CurrentUserEmails/.testdata/List_Should_Return_The_Emails.json")]
        public async Task List_Should_Return_The_Emails(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), "user/emails").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var emails = await sut.List();

            emails.ShouldNotBeNull();
        }

        [Fact]
        public async Task Remove_Should_Remove_The_Email_And_Return_Nothing()
        {
            var handlerStub = new MockHttpMessageHandler();

            const string email = "mike.benson@buddy.works";

            var url = new Uri(new Uri(BaseUrl), $"user/emails/{email}").ToString();

            handlerStub.When(HttpMethod.Delete, url).Respond(HttpStatusCode.NoContent);

            var sut = CreateClient(handlerStub);

            await sut.Remove(email);
        }

        private static ICurrentUserEmailsClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new CurrentUserEmailsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), "PAT")));
        }
    }
}