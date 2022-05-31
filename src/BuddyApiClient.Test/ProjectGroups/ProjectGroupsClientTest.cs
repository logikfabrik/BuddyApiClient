namespace BuddyApiClient.Test.ProjectGroups
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Groups.Models;
    using BuddyApiClient.PermissionSets.Models;
    using BuddyApiClient.ProjectGroups;
    using BuddyApiClient.ProjectGroups.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class ProjectGroupsClientTest
    {
        private static IProjectGroupsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectGroupsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"ProjectGroups/.testdata/Add_Should_Add_And_Return_The_Project_Group.json")]
            public async Task Should_Add_And_Return_The_Project_Group(string responseJson)
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
            [FileData(@"ProjectGroups/.testdata/Get_Should_Return_The_Project_Group_If_It_Exists.json")]
            public async Task Should_Return_The_Project_Group_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups/2").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroup = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new GroupId(2));

                projectGroup.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Project_Group_Does_Not_Exist()
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
            [FileData(@"ProjectGroups/.testdata/List_Should_Return_Project_Groups_If_Any_Exists.json")]
            public async Task Should_Return_Project_Groups_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/groups").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projectGroups = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                projectGroups?.Groups.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"ProjectGroups/.testdata/List_Should_Not_Return_Project_Groups_If_None_Exist.json")]
            public async Task Should_Not_Return_Project_Groups_If_None_Exist(string responseJson)
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
            public async Task Should_Remove_The_Project_Group_And_Return_Nothing()
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
            [FileData(@"ProjectGroups/.testdata/Update_Should_Update_And_Return_The_Project_Group.json")]
            public async Task Should_Update_And_Return_The_Project_Group(string responseJson)
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