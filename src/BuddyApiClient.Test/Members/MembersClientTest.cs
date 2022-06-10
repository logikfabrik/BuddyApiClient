namespace BuddyApiClient.Test.Members
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class MembersClientTest
    {
        private static IMembersClient CreateClient(MockHttpMessageHandler handler)
        {
            return new MembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"Members/.testdata/Add_Should_AddTheMember.json")]
            public async Task Should_AddTheMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var member = await sut.Add(new Domain("buddy"), new AddMember("mike.benson@buddy.works"));

                member.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Members/.testdata/Get_Should_ReturnTheMember.json")]
            public async Task Should_ReturnTheMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var member = await sut.Get(new Domain("buddy"), new MemberId(1));

                member.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheMemberDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/members/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new MemberId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Members/.testdata/List_Should_ReturnTheMembers.json")]
            public async Task Should_ReturnTheMembers(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var members = await sut.List(new Domain("buddy"));

                members?.Members.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Members/.testdata/List_Should_ReturnNoMembers_When_NoneExist.json")]
            public async Task Should_ReturnNoMembers_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var members = await sut.List(new Domain("buddy"));

                members?.Members.Should().BeEmpty();
            }
        }

        public sealed class ListAll
        {
            [Theory]
            [FileData(@"Members/.testdata/ListAll_Should_ReturnTheMembers.json")]
            public async Task Should_ReturnTheMembers(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/members?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var members = new List<MemberSummary>();

                var collectionQuery = new ListMembersQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), collectionQuery, (_, response, _) =>
                {
                    members.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                members.Should().NotBeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_RemoveTheMember()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/members/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove(new Domain("buddy"), new MemberId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"Members/.testdata/Update_Should_UpdateTheMember.json")]
            public async Task Should_UpdateTheMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var member = await sut.Update(new Domain("buddy"), new MemberId(1), new UpdateMember());

                member.Should().NotBeNull();
            }
        }
    }
}