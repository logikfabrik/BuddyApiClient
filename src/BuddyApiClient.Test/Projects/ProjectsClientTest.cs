namespace BuddyApiClient.Test.Projects
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
    using BuddyApiClient.Projects;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class ProjectsClientTest
    {
        private static IProjectsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ProjectsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), null)));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Projects/.testdata/Create_Should_Create_And_Return_The_Project.json")]
            public async Task Should_Create_And_Return_The_Project(string responseJson)
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
            [FileData(@"Projects/.testdata/Get_Should_Return_The_Project_If_It_Exists.json")]
            public async Task Should_Return_The_Project_If_It_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var project = await sut.Get(new Domain("buddy"), new ProjectName("company-website"));

                project.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_If_The_Project_Does_Not_Exist()
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
            [FileData(@"Projects/.testdata/List_Should_Return_Projects_If_Any_Exists.json")]
            public async Task Should_Return_Projects_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = await sut.List(new Domain("buddy"));

                projects?.Projects.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Projects/.testdata/List_Should_Not_Return_Projects_If_None_Exist.json")]
            public async Task Should_Not_Return_Projects_If_None_Exist(string responseJson)
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
            [FileData(@"Projects/.testdata/ListAll_Should_Return_Projects_If_Any_Exists.json")]
            public async Task Should_Return_Projects_If_Any_Exists(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects?page={PageIterator.DefaultPageIndex}&per_page={PageIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = new List<ProjectSummary>();

                var pageQuery = new ListProjectsQuery();

                var pageIterator = sut.ListAll(new Domain("buddy"), pageQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                projects.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Projects/.testdata/ListAll_Should_Not_Return_Projects_If_None_Exist.json")]
            public async Task Should_Not_Return_Projects_If_None_Exist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects?page={PageIterator.DefaultPageIndex}&per_page={PageIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var projects = new List<ProjectSummary>();

                var pageQuery = new ListProjectsQuery();

                var pageIterator = sut.ListAll(new Domain("buddy"), pageQuery, (_, response, _) =>
                {
                    projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                    return Task.FromResult(true);
                });

                await pageIterator.Iterate();

                projects.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_Delete_The_Project_And_Return_Nothing()
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
            [FileData(@"Projects/.testdata/Update_Should_Update_And_Return_The_Project.json")]
            public async Task Should_Update_And_Return_The_Project(string responseJson)
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