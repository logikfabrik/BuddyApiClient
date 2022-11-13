using BuddyApiClient.Core;
using RichardSzalay.MockHttp;

namespace BuddyApiClient.Test.Pipelines
{
    using System.Net.Mime;
    using BuddyApiClient.Pipelines;
    using BuddyApiClient.Pipelines.Models.Request;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;

    public sealed class PipelinesClientTest
    {
        private static IPipelinesClient CreateClient(MockHttpMessageHandler handler)
        {
            return new PipelinesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Pipelines/.testdata/Create_Should_CreateThePipeline.json")]
            public async Task Should_CreateTheProject(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var pipeline = await sut.Create(new Domain("buddy"), new ProjectName("company-website"), new CreateOnClickPipeline("Tests"));

                pipeline.Should().NotBeNull();
            }
        }
    }
}
