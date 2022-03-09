namespace BuddyApiClient.Test.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class MembersClientTest
    {
        private const string BaseUrl = "https://api.buddy.works";
        private const string Domain = "buddy";
        private const int MemberId = 1;

        [Theory]
        [FileData(@"Members/.testdata/Add_Should_Add_And_Return_The_Added_Member.json")]
        public async Task Add_Should_Add_And_Return_The_Added_Member(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members").ToString();

            handlerStub.When(HttpMethod.Post, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var member = await sut.Add(Domain, new AddMember("mike.benson@buddy.works"));

            member.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Members/.testdata/Get_For_Member_That_Exists_Should_Return_The_Member.json")]
        public async Task Get_For_Member_That_Exists_Should_Return_The_Member(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members/{MemberId}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var member = await sut.Get(Domain, MemberId);

            member.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_For_Member_That_Does_Not_Exist_Should_Throw()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members/{MemberId}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(HttpStatusCode.NotFound);

            var sut = CreateClient(handlerStub);

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, MemberId));

            e.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Members/.testdata/List_Should_Return_The_Members.json")]
        public async Task List_Should_Return_The_Members(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var members = await sut.List(Domain);

            members.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Members/.testdata/ListAll_Should_Return_The_Members.json")]
        public async Task ListAll_Should_Return_The_Members(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members?page={PageIterator.DefaultPageIndex}&per_page={PageIterator.DefaultPageSize}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var members = new List<MemberSummary>();

            var pageQuery = new ListMembersQuery();

            var pageIterator = sut.ListAll(Domain, pageQuery, (_, response, _) =>
            {
                members.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                return Task.FromResult(true);
            });

            await pageIterator.Iterate();

            members.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Remove_Should_Remove_The_Member_And_Return_Nothing()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members/{MemberId}").ToString();

            handlerStub.When(HttpMethod.Delete, url).Respond(HttpStatusCode.NoContent);

            var sut = CreateClient(handlerStub);

            await sut.Remove(Domain, MemberId);
        }

        [Theory]
        [FileData(@"Members/.testdata/Update_Should_Update_And_Return_The_Member.json")]
        public async Task Update_Should_Update_And_Return_The_Member(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/members/{MemberId}").ToString();

            handlerStub.When(HttpMethod.Patch, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var member = await sut.Update(Domain, MemberId, new UpdateMember());

            member.ShouldNotBeNull();
        }

        private static IMembersClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new MembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), null)));
        }
    }
}