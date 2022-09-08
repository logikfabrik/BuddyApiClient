namespace BuddyApiClient.Test.Actions.Models
{
    using BuddyApiClient.Actions.Models;

    public sealed class TriggerTimeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TriggerTimeJsonConverter.OnEveryExecutionAsJson, TriggerTime.OnEveryExecution)]
            [InlineData(TriggerTimeJsonConverter.OnFailureAsJson, TriggerTime.OnFailure)]
            [InlineData(TriggerTimeJsonConverter.OnWarningAsJson, TriggerTime.OnWarning)]
            [InlineData(TriggerTimeJsonConverter.OnWaitForApproveAsJson, TriggerTime.OnWaitForApprove)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, TriggerTime expected)
            {
                var enumValue = TriggerTimeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerTimeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(TriggerTime.OnEveryExecution, TriggerTimeJsonConverter.OnEveryExecutionAsJson)]
            [InlineData(TriggerTime.OnFailure, TriggerTimeJsonConverter.OnFailureAsJson)]
            [InlineData(TriggerTime.OnWarning, TriggerTimeJsonConverter.OnWarningAsJson)]
            [InlineData(TriggerTime.OnWaitForApprove, TriggerTimeJsonConverter.OnWaitForApproveAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(TriggerTime enumValue, string expected)
            {
                var json = TriggerTimeJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerTimeJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
