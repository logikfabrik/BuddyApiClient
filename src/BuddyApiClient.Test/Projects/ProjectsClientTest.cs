namespace BuddyApiClient.Test.Projects
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class ProjectsClientTest
    {
        private static IProjectsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(new Uri("https://api.buddy.works"), string.Empty, handler.ToHttpClient())));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Projects/.testdata/Create_Should_CreateTheProject.json")]
            public async Task Should_CreateTheProject(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var project = await sut.Create(new Domain("buddy"), new CreateProject("Landing page"));

                project.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Projects/.testdata/Get_Should_ReturnTheProject.json")]
            public async Task Should_ReturnTheProject(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var project = await sut.Get(new Domain("buddy"), new ProjectName("company-website"));

                project.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheProjectDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new ProjectName("company-website")));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Projects/.testdata/List_Should_ReturnTheProjects.json")]
            public async Task Should_ReturnTheProjects(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = await sut.List(new Domain("buddy"));

                projects?.Projects.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Projects/.testdata/List_Should_ReturnNoProjects_When_NoneExist.json")]
            public async Task Should_ReturnNoProjects_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = await sut.List(new Domain("buddy"));

                projects?.Projects.Should().BeEmpty();
            }
        }

        public sealed class ListAll
        {
            [Theory]
            [FileData(@"Projects/.testdata/ListAll_Should_ReturnTheProjects.json")]
            public async Task Should_ReturnTheProjects(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = new List<ProjectSummary>();

                var collectionQuery = new ListProjectsQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), collectionQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                projects.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Projects/.testdata/ListAll_Should_ReturnNoProjects_When_NoneExist.json")]
            public async Task Should_ReturnNoProjects_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = new List<ProjectSummary>();

                var collectionQuery = new ListProjectsQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), collectionQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                projects.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_DeleteTheProject()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/projects/company-website").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete(new Domain("buddy"), new ProjectName("company-website"));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"Projects/.testdata/Update_Should_UpdateTheProject.json")]
            public async Task Should_UpdateTheProject(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/projects/company-website").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var project = await sut.Update(new Domain("buddy"), new ProjectName("company-website"), new UpdateProject());

                project.Should().NotBeNull();
            }
        }
    }
}