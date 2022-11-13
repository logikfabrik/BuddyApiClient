namespace BuddyApiClient.Test.Pipelines.Models
{
    using BuddyApiClient.Pipelines.Models;

    public sealed class TriggerModeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TriggerModeJsonConverter.ClickAsJson, TriggerMode.Click)]
            [InlineData(TriggerModeJsonConverter.EventAsJson, TriggerMode.Event)]
            [InlineData(TriggerModeJsonConverter.ScheduleAsJson, TriggerMode.Schedule)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, TriggerMode expected)
            {
                var enumValue = TriggerModeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerModeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(TriggerMode.Click, TriggerModeJsonConverter.ClickAsJson)]
            [InlineData(TriggerMode.Event, TriggerModeJsonConverter.EventAsJson)]
            [InlineData(TriggerMode.Schedule, TriggerModeJsonConverter.ScheduleAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(TriggerMode enumValue, string expected)
            {
                var json = TriggerModeJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => TriggerModeJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
