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
    using BuddyApiClient.Projects.Models.Request;
    using BuddyApiClient.Projects.Models.Response;
    using RichardSzalay.MockHttp;
    using Shouldly;
    using Xunit;

    public sealed class ProjectsClientTest
    {
        private const string BaseUrl = "https://api.buddy.works";
        private const string Domain = "buddy";
        private const string ProjectName = "company-website";

        [Theory]
        [FileData(@"Projects/.testdata/Create_Should_Create_And_Return_The_Created_Project.json")]
        public async Task Create_Should_Create_And_Return_The_Created_Project(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects").ToString();

            handlerStub.When(HttpMethod.Post, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var project = await sut.Create(Domain, new CreateProject("Landing page"));

            project.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Projects/.testdata/Get_For_Project_That_Exists_Should_Return_The_Project.json")]
        public async Task Get_For_Project_That_Exists_Should_Return_The_Project(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects/{ProjectName}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var project = await sut.Get(Domain, ProjectName);

            project.ShouldNotBeNull();
        }

        [Fact]
        public async Task Get_For_Project_That_Does_Not_Exist_Should_Throw()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects/{ProjectName}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(HttpStatusCode.NotFound);

            var sut = CreateClient(handlerStub);

            var e = await Assert.ThrowsAsync<HttpRequestException>(() => sut.Get(Domain, ProjectName));

            e.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Projects/.testdata/List_Should_Return_The_Projects.json")]
        public async Task List_Should_Return_The_Projects(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var members = await sut.List(Domain);

            members.ShouldNotBeNull();
        }

        [Theory]
        [FileData(@"Projects/.testdata/ListAll_Should_Return_The_Projects.json")]
        public async Task ListAll_Should_Return_The_Projects(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects?page={PageIterator.DefaultPageIndex}&per_page={PageIterator.DefaultPageSize}").ToString();

            handlerStub.When(HttpMethod.Get, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var projects = new List<ProjectSummary>();

            var pageQuery = new ListProjectsQuery();

            var pageIterator = sut.ListAll(Domain, pageQuery, (_, response, _) =>
            {
                projects.AddRange(response?.Projects ?? Enumerable.Empty<ProjectSummary>());

                return Task.FromResult(true);
            });

            await pageIterator.Iterate();

            projects.Count.ShouldBe(1);
        }

        [Fact]
        public async Task Delete_Should_Delete_The_Project_And_Return_Nothing()
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects/{ProjectName}").ToString();

            handlerStub.When(HttpMethod.Delete, url).Respond(HttpStatusCode.NoContent);

            var sut = CreateClient(handlerStub);

            await sut.Delete(Domain, ProjectName);
        }

        [Theory]
        [FileData(@"Projects/.testdata/Update_Should_Update_And_Return_The_Project.json")]
        public async Task Update_Should_Update_And_Return_The_Permission_Set(string responseJson)
        {
            var handlerStub = new MockHttpMessageHandler();

            var url = new Uri(new Uri(BaseUrl), $"workspaces/{Domain}/projects/{ProjectName}").ToString();

            handlerStub.When(HttpMethod.Patch, url).Respond(MediaTypeNames.Application.Json, responseJson);

            var sut = CreateClient(handlerStub);

            var permissionSet = await sut.Update(Domain, ProjectName, new UpdateProject());

            permissionSet.ShouldNotBeNull();
        }


        private static IProjectsClient CreateClient(MockHttpMessageHandler handlerStub)
        {
            return new ProjectsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handlerStub.ToHttpClient(), new Uri(BaseUrl), null)));
        }
    }
}