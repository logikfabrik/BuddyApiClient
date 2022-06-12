namespace BuddyApiClient.Test.Variables.Models.Request
{
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Variables.Models.Request;

    public sealed class ListVariablesQueryTest
    {
        public sealed class Build
        {
            [Theory]
            [InlineData("myproject", "?projectName=myproject")]
            public void Should_ReturnQuery_When_ProjectNameIsSet(string projectName, string expected)
            {
                var sut = new ListVariablesQuery { ProjectName = new ProjectName(projectName) };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }

            [Theory]
            [InlineData(1, "?pipelineId=1")]
            public void Should_ReturnQuery_When_PipelineIdIsSet(int pipelineId, string expected)
            {
                var sut = new ListVariablesQuery { PipelineId = new PipelineId(pipelineId) };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }

            [Theory]
            [InlineData(1, "?actionId=1")]
            public void Should_ReturnQuery_When_ActionIdIsSet(int actionId, string expected)
            {
                var sut = new ListVariablesQuery { ActionId = new ActionId(actionId) };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }
        }
    }
}