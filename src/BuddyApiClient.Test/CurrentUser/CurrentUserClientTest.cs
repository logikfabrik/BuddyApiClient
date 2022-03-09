namespace BuddyApiClient.Test.CurrentUser
{
    using System;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUser.Models.Request;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class CurrentUserClientTest
    {
        private const string BaseUrl = "https://api.buddy.works";

        [Theory]
        [FileData(@"CurrentUser/.testdata/Get_Should_Return_The_Current_User.json")]
        public async Task Get_Should_Return_The_Current_User(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), "user").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var currentUser = await sut.Get();

            currentUser.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"CurrentUser/.testdata/Update_Should_Update_And_Return_The_Current_User.json")]
        public async Task Update_Should_Update_And_Return_The_Current_User(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), "user").ToString();

            handlerStub.When(HttpMethod.Patch, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var currentUser = await sut.Update(new UpdateUser("Mike Benson"));

            currentUser.ShouldNotBeNull();
        }

        private static ICurrentUserClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new CurrentUserClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), null)));
        }
    }
}