namespace BuddyApiClient.Test.Pipelines.Models
{
    using BuddyApiClient.Pipelines.Models;

    public sealed class ExecutionStatusJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(ExecutionStatusJsonConverter.SuccessfulAsJson, ExecutionStatus.Successful)]
            [InlineData(ExecutionStatusJsonConverter.FailedAsJson, ExecutionStatus.Failed)]
            [InlineData(ExecutionStatusJsonConverter.InProgressAsJson, ExecutionStatus.InProgress)]
            [InlineData(ExecutionStatusJsonConverter.EnqueuedAsJson, ExecutionStatus.Enqueued)]
            [InlineData(ExecutionStatusJsonConverter.SkippedAsJson, ExecutionStatus.Skipped)]
            [InlineData(ExecutionStatusJsonConverter.TerminatedAsJson, ExecutionStatus.Terminated)]
            [InlineData(ExecutionStatusJsonConverter.InitialAsJson, ExecutionStatus.Initial)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, ExecutionStatus expected)
            {
                var enumValue = ExecutionStatusJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => ExecutionStatusJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(ExecutionStatus.Successful, ExecutionStatusJsonConverter.SuccessfulAsJson)]
            [InlineData(ExecutionStatus.Failed, ExecutionStatusJsonConverter.FailedAsJson)]
            [InlineData(ExecutionStatus.InProgress, ExecutionStatusJsonConverter.InProgressAsJson)]
            [InlineData(ExecutionStatus.Enqueued, ExecutionStatusJsonConverter.EnqueuedAsJson)]
            [InlineData(ExecutionStatus.Skipped, ExecutionStatusJsonConverter.SkippedAsJson)]
            [InlineData(ExecutionStatus.Terminated, ExecutionStatusJsonConverter.TerminatedAsJson)]
            [InlineData(ExecutionStatus.Initial, ExecutionStatusJsonConverter.InitialAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(ExecutionStatus enumValue, string expected)
            {
                var json = ExecutionStatusJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => ExecutionStatusJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
