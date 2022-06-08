namespace BuddyApiClient.Test.Variables.Models.Request
{
    using BuddyApiClient.Actions.Models;
    using BuddyApiClient.Pipelines.Models;
    using BuddyApiClient.Projects.Models;
    using BuddyApiClient.Variables.Models.Request;

    public sealed class CreateVariableTest
    {
        private static CreateVariable CreateRequest(IScope scope)
        {
            return new CreateVariable(scope, "my_var", "some value");
        }

        public sealed class Key
        {
            [Fact]
            public void Should_Return_A_Key()
            {
                const string key = "my_var";

                var sut = new CreateVariable(key, "some value");

                sut.Key.Should().Be(key);
            }
        }

        public sealed class Value
        {
            [Fact]
            public void Should_Return_A_Value()
            {
                const string value = "some value";

                var sut = new CreateVariable("my_var", value);

                sut.Value.Should().Be(value);
            }
        }

        public sealed class Project
        {
            [Fact]
            public void Should_Return_A_Project()
            {
                var scope = new BuddyApiClient.Variables.Models.Request.Project { Name = new ProjectName("myproject") };

                var sut = CreateRequest(scope);

                sut.Project.Should().Be(scope);
            }
        }

        public sealed class Pipeline
        {
            [Fact]
            public void Should_Return_A_Pipeline()
            {
                var scope = new BuddyApiClient.Variables.Models.Request.Pipeline { Id = new PipelineId(1) };

                var sut = CreateRequest(scope);

                sut.Pipeline.Should().Be(scope);
            }
        }

        public sealed class Action
        {
            [Fact]
            public void Should_Return_An_Action()
            {
                var scope = new BuddyApiClient.Variables.Models.Request.Action { Id = new ActionId(1) };

                var sut = CreateRequest(scope);

                sut.Action.Should().Be(scope);
            }
        }
    }
}