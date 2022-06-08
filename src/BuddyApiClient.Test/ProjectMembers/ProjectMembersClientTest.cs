namespace BuddyApiClient.Test.ProjectMembers
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models;
    using BuddyApiClient.Members.Models.Request;
    using BuddyApiClient.Members.Models.Response;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectMembers;
    using BuddyApiClient.ProjectMembers.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class ProjectMembersClientTest
    {
        private static IProjectMembersClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectMembersClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Add_Should_AddTheProjectMember.json")]
            public async Task Should_AddTheProjectMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Add(new Domain("buddy"), new ProjectName("company-website"), new AddProjectMember(new PermissionSet { Id = new PermissionSetId(2) }) { MemberId = new MemberId(2) });

                projectMember.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Get_Should_ReturnTheProject.json")]
            public async Task Should_ReturnTheProject(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1));

                projectMember.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectMemberDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/List_Should_ReturnTheProjectMembers.json")]
            public async Task Should_ReturnTheProjectMembers(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectMembers?.Members.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"ProjectMembers/.testdata/List_Should_ReturnNoProjectMembers_When_NoneExist.json")]
            public async Task Should_ReturnNoProjectMembers_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/members").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectMembers?.Members.Should().BeEmpty();
            }
        }

        public sealed class ListAll
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/ListAll_Should_ReturnTheProjectMembers.json")]
            public async Task Should_ReturnTheProjectMembers(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects/company-website/members?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMembers = new List<MemberSummary>();

                var collectionQuery = new ListMembersQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), new ProjectName("company-website"), collectionQuery, (_, response, _) =>
                {
                    projectMembers.AddRange(response?.Members ?? Enumerable.Empty<MemberSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                projectMembers.Should().NotBeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_RemoveTheProjectMember()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"ProjectMembers/.testdata/Update_Should_UpdateTheProjectMember.json")]
            public async Task Should_UpdateTheProjectMember(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/projects/company-website/members/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectMember = await sut.Update(new Domain("buddy"), new ProjectName("company-website"), new MemberId(1), new UpdateProjectMember(new PermissionSet { Id = new PermissionSetId(1) }));

                projectMember.Should().NotBeNull();
            }
        }
    }
}