namespace BuddyApiClient.Test.Pipelines
{
    using System.Net;
    using System.Net.Mime;
    using BuddyApiClient.Core;
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Pipelines;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Pipelines.Models.Response;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class PipelinesClientTest
    {
        private static IPipelinesClient CreateClient(MockHttpMessageHandler handler)
        {
            return new PipelinesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Create
        {
            [Theory]
            [FileTextData(@"Pipelines/.testdata/Create_Should_CreateThePipeline.json")]
            public async Task Should_CreateThePipeline(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipeline = await sut.Create(new Domain("buddy"), new ProjectName("company-website"), new CreateOnClickPipeline("Tests"));

                pipeline.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileTextData(@"Pipelines/.testdata/Get_Should_ReturnThePipeline.json")]
            public async Task Should_ReturnThePipeline(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipeline = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2));

                pipeline.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_ThePipelineDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileTextData(@"Pipelines/.testdata/List_Should_ReturnThePipelines.json")]
            public async Task Should_ReturnThePipelines(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipelines = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                pipelines?.Pipelines.Should().NotBeEmpty();
            }

            [Theory]
            [FileTextData(@"Pipelines/.testdata/List_Should_ReturnNoPipelines_When_NoneExist.json")]
            public async Task Should_ReturnNoPipelines_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipelines = await sut.List(new Domain("buddy"), new ProjectName("company-website"));

                pipelines?.Pipelines.Should().BeEmpty();
            }
        }

        public sealed class ListAll
        {
            [Theory]
            [FileTextData(@"Pipelines/.testdata/ListAll_Should_ReturnThePipelines.json")]
            public async Task Should_ReturnThePipelines(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipelines = new List<PipelineSummary>();

                var collectionQuery = new ListPipelinesQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), new ProjectName("company-website"), collectionQuery, (_, response, _) =>
                {
                    pipelines.AddRange(response?.Pipelines ?? Enumerable.Empty<PipelineSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                pipelines.Should().NotBeEmpty();
            }

            [Theory]
            [FileTextData(@"Pipelines/.testdata/ListAll_Should_ReturnNoPipelines_When_NoneExist.json")]
            public async Task Should_ReturnNoPipelines_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, $"https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines?page={CollectionIterator.DefaultPageIndex}&per_page={CollectionIterator.DefaultPageSize}").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipelines = new List<PipelineSummary>();

                var collectionQuery = new ListPipelinesQuery();

                var collectionIterator = sut.ListAll(new Domain("buddy"), new ProjectName("company-website"), collectionQuery, (_, response, _) =>
                {
                    pipelines.AddRange(response?.Pipelines ?? Enumerable.Empty<PipelineSummary>());

                    return Task.FromResult(true);
                });

                await collectionIterator.Iterate();

                pipelines.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_DeleteThePipeline()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }
    }
}
