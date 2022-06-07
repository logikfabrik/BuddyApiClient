namespace BuddyApiClient.Test.Variables
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Net.Mime;
    using System.Threading.Tasks;
    using BuddyApiClient.Core;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Test.Testing;
    using BuddyApiClient.Variables;
    using BuddyApiClient.Variables.Models;
    using BuddyApiClient.Variables.Models.Request;
    using FluentAssertions;
    using RichardSzalay.MockHttp;
    using Xunit;

    public sealed class VariablesClientTest
    {
        private static IVariablesClient CreateClient(MockHttpMessageHandler handler)
        {
            return new VariablesClient(new Lazy<HttpClientFacade>(HttpClientFacadeFactory.Create(handler.ToHttpClient(), new Uri("https://api.buddy.works"), string.Empty)));
        }

        public sealed class Create
        {
            [Theory]
            [FileData(@"Variables/.testdata/Create_Should_CreateTheVariable.json")]
            public async Task Should_CreateTheVariable(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/variables").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var variable = await sut.Create(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new CreateVariable(new Project { Name = new ProjectName("myproject") }, "my_var", "some value"));

                variable.Should().NotBeNull();
            }

            [Theory]
            [FileData(@"Variables/.testdata/Create_Should_CreateTheSshKeyVariable.json")]
            public async Task Should_CreateTheSshKeyVariable(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Post, "https://api.buddy.works/workspaces/buddy/variables").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                const string key = "-----BEGIN RSA PRIVATE KEY-----MIIEpAIBAAKCAQEAz/qH9/t6jCVuGPHcDDvnyqtasOlsjZOgF9dtRioNG93GrLPzDoi+SgUBzesoKXb1CGOW1wKAzLSADzuFMKwSydxCBVwziGZ33F+XsnHGKX3mpx8rRZV2UWNHRtJK3wmJHfEsIDHznPcbJT2I++cK36yGGpiNtzItfdvhExeFeXJVjp/BrZS2D4ixBu5VjqFe5XskIN6N//QCgfwxTwmnhoGBnB8xiWnDCHHUP/rf6Tr6FPneWt2zE5H53kqApT1boDN/VgaltptjW5zh6DnN6S9zhMBY4OkQoQQ1pCWrH56uryYaqTdslkoP4F1Hos9DkkCwCce8J/uFLE6PVY7Xk6JiQIDAQABAoIBAEOd5TTXetkzlh7gCzWjCFIY7d0SAaXdNCg9Fq/IYRzXT6sEwKYyt3g0WqbY1FkpaeojzXIH7tNDE/nabcWuNsC77oYgy3m8J3B/LQ6zbtPCirpPZ6vS8/UBmQFdRJ1YnwbvZ4aUnpZiu7+lODXf5BESDUpe75alEt7uzZn2K8cZQRb3O8cIpv4KIqxA4kpf+VkJ/1l8pLLx/cG85pxuLy6RlV8IlerOq7ZK85afdGPM9VKK+WNvL7803/Egt5Zs8p+Uc0W8ZXHhEzje4UreYE83AgU3EMSAJp69JnYQGGQsDHAC63GqUBSpWlI1gImZ9shD2rXrHkiiTnltcTWHx0CgYEA+UCU3rB+AoptMA/R9HxfJlgFacQPCnbSeziksTV0lyd44LiXJBBbchTKFg4sxBSuPcq0+rKnUxPcNNRbmw9Wvo+amqeyRvXDuXRXTxxmFpqDQehMW1A3Hav3aVfcarGcQAiZJAOQz1Hhma05Xy2HQsqHOqqVB7FktNcoIKccRgcCgYEA1ZvoHtkKG7ZAI+XJZmJUnsimWfH67prbAnlfBKGz/gDxA0REmTsAT92+T5jiXM6VyHj6G2GGVILZoG1L1YDecF/GLvhb3QmW8cwKnaX84XoI0oKYaNGnzmZIMcKQKZdFUZfbXBTT4I/xx1I+/aYRkp2WZBE5Emfg+RfZKkLT+8CgYBkyCMpo81fs58Qk1O3M4FkMxPxzVo4KWh8jKbUV3hplX9V1b83VDDQdfaB0qIibRA1r+MGSuJgFdHtOdSsRGHOnm6f0/y2ynq4Sv1ehIoWy6UVChuNtHKEsdrZImiCT4xxK8jYEgUEfwQsQil3fY46iG+DXiPiN20T3gdgpJ/EwKBgQCBon2LgxJ0YPWqE4FXpmNOfd0fBxDPj6FMbhYxPGV8yFt8LQkoqTr2PU+LBPCTfDhAktLOnTAlx8eFae804mGcdCY3P4Z/2lCRMGBV1OVAwEpyaDPGr41jglQhJUpek9g32h59z0ZwIU7Na0Nozm35cqVCk6CLnva013rhHwojdwKBgQCd1NBOpiE2/R7JsmK8crt0NH+Q9RFPKfnv4itvsuy9dRtDV04jl60jbzVC+EKD4M6jysijNF4TDTfXljmHjkx8oGW9PTZQ88TQm8maOO1mKuQAWAu3AeCIZXqdmiQQASD+p6+goJLgpKuKQJQ2LQJZgi1gvw+jBeTJPILXQgt1Q==-----END RSA PRIVATE KEY-----";

                var variable = await sut.Create(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new CreateSshKeyVariable("id_my_key", key, FilePlace.Container, "id_my_key", "~/.ssh/id_my_key", new FilePermission("600")) { Description = "My Server Key", Encrypted = true });

                variable.Should().NotBeNull();
            }
        }

        public sealed class Get
        {
            [Theory]
            [FileData(@"Variables/.testdata/Get_Should_ReturnTheVariable.json")]
            public async Task Should_ReturnTheVariable(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/variables/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var variable = await sut.Get(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new VariableId(1));

                variable.Should().NotBeNull();
            }

            [Fact]
            public async Task Should_Throw_When_TheVariableDoesNotExist()
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/variables/1").Respond(HttpStatusCode.NotFound);

                var sut = CreateClient(handlerStub);

                var act = FluentActions.Awaiting(() => sut.Get(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new VariableId(1)));

                (await act.Should().ThrowAsync<HttpRequestException>()).And.StatusCode.Should().Be(HttpStatusCode.NotFound);
            }
        }

        public sealed class List
        {
            [Theory]
            [FileData(@"Variables/.testdata/List_Should_ReturnTheVariables.json")]
            public async Task Should_ReturnTheVariables(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/variables").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var variables = await sut.List(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new ListVariablesQuery());

                variables?.Variables.Should().NotBeEmpty();
            }

            [Theory]
            [FileData(@"Variables/.testdata/List_Should_ReturnNoVariables_When_NoneExist.json")]
            public async Task Should_ReturnNoVariables_When_NoneExist(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Get, "https://api.buddy.works/workspaces/buddy/variables").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var variables = await sut.List(new BuddyApiClient.Workspaces.Models.Domain("buddy"));

                variables?.Variables.Should().BeEmpty();
            }
        }

        public sealed class Delete
        {
            [Fact]
            public async Task Should_DeleteTheVariable()
            {
                var handlerMock = new MockHttpMessageHandler();

                handlerMock.Expect(HttpMethod.Delete, "https://api.buddy.works/workspaces/buddy/variables/1").Respond(HttpStatusCode.NoContent);

                var sut = CreateClient(handlerMock);

                await sut.Delete(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new VariableId(1));

                handlerMock.VerifyNoOutstandingExpectation();
            }
        }

        public sealed class Update
        {
            [Theory]
            [FileData(@"Variables/.testdata/Update_Should_UpdateTheVariable.json")]
            public async Task Should_UpdateTheVariable(string responseJson)
            {
                var handlerStub = new MockHttpMessageHandler();

                handlerStub.When(HttpMethod.Patch, "https://api.buddy.works/workspaces/buddy/variables/1").Respond(MediaTypeNames.Application.Json, responseJson);

                var sut = CreateClient(handlerStub);

                var variable = await sut.Update(new BuddyApiClient.Workspaces.Models.Domain("buddy"), new VariableId(1), new UpdateVariable());

                variable.Should().NotBeNull();
            }
        }
    }
}