namespace BuddyApiClient.Test.ProjectGroups
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class ProjectGroupsClientTest
    {
        private static IProjectGroupsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectGroupsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"ProjectGroups/.testdata/Add_Should_AddTheProjectGroup.json")]
            public async Task Should_AddTheProjectGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroup = await sut.Add(new Domain("buddy"), new ProjectName("company-website"), new AddProjectGroup(new PermissionSet { Id = new PermissionSetId(2) }) { GroupId = new GroupId(2) });

                projectGroup.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"ProjectGroups/.testdata/Get_Should_ReturnTheProjectGroup.json")]
            public async Task Should_ReturnTheProjectGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups/2").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroup = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new GroupId(2));

                projectGroup.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectGroupDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups/2").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new ProjectName("company-website"), new GroupId(2)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"ProjectGroups/.testdata/List_Should_ReturnTheProjectGroups.json")]
            public async Task Should_ReturnTheProjectGroups(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroups = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectGroups?.Groups.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"ProjectGroups/.testdata/List_Should_ReturnNoProjectGroups_When_NoneExist.json")]
            public async Task Should_ReturnNoProjectGroups_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroups = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectGroups?.Groups.Should().BeEmpty();
            }
        }

        public sealed class Remove
        {
            [Fact]
            public async Task Should_RemoveTheProjectGroup()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups/2").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Remove(new Domain("buddy"), new ProjectName("company-website"), new GroupId(2));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"ProjectGroups/.testdata/Update_Should_UpdateTheProjectGroup.json")]
            public async Task Should_UpdateTheProjectGroup(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups/2").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroup = await sut.Update(new Domain("buddy"), new ProjectName("company-website"), new GroupId(2), new UpdateProjectGroup(new PermissionSet { Id = new PermissionSetId(1) }));

                projectGroup.Should().NotBeNull();
            }
        }
    }
}