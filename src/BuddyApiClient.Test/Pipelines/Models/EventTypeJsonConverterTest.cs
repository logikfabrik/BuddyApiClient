namespace BuddyApiClient.Test.Pipelines.Models
{
    using BuddyApiClient.Pipelines.Models;

    public sealed class EventTypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(EventTypeJsonConverter.PushAsJson, EventType.Push)]
            [InlineData(EventTypeJsonConverter.CreateReferenceAsJson, EventType.CreateReference)]
            [InlineData(EventTypeJsonConverter.DeleteReferenceAsJson, EventType.DeleteReference)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, EventType expected)
            {
                var enumValue = EventTypeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => EventTypeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(EventType.Push, EventTypeJsonConverter.PushAsJson)]
            [InlineData(EventType.CreateReference, EventTypeJsonConverter.CreateReferenceAsJson)]
            [InlineData(EventType.DeleteReference, EventTypeJsonConverter.DeleteReferenceAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(EventType enumValue, string expected)
            {
                var json = EventTypeJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => EventTypeJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}
