namespace BuddyApiClient.Test.Actions
{
    using System.Net.Mime;
    using BuddyApiClient.Actions;
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Actions.Models.Request;
    using BuddyApiClient.Core;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Workspaces.Models;
    using RichardSzalay.MockHttp;

    public sealed class ActionsClientTest
    {
        private static IActionsClient CreateClient(MockHttpMessageHandler handler)
        {
            return new ActionsClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(string.Empty, new Uri("https://api.buddy.works"), handler.ToHttpClient())));
        }

        public sealed class Add
        {
            [Theory]
            [FileData(@"Actions/.testdata/Add_Should_AddTheSleepAction.json")]
            public async Task Should_AddTheSleepAction(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2/actions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var action = await sut.Add(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2), new AddSleepAction("Sleep well, darling") { SleepInSeconds = 600 });

                action.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Actions/.testdata/Get_Should_ReturnTheAction.json")]
            public async Task Should_ReturnTheAction(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2/actions/5").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var action = await sut.Get(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2), new ActionId(5));

                action.Should().NotBeNull();
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Actions/.testdata/List_Should_ReturnTheActions.json")]
            public async Task Should_ReturnTheProjects(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2/actions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var actions = await sut.List(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2));

                actions?.Actions.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Actions/.testdata/List_Should_ReturnNoActions_When_NoneExist.json")]
            public async Task Should_ReturnNoProjects_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/projects/company-website/pipelines/2/actions").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var actions = await sut.List(new Domain("buddy"), new ProjectName("company-website"), new PipelineId(2));

                actions?.Actions.Should().BeEmpty();
            }
        }
    }
}