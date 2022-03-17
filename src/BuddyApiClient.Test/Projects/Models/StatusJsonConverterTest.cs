namespace BuddyApiClient.Test.Projects.Models
{
    using System;
    using BuddyApiClient.Projects.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class StatusJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(StatusJsonConverter.ActiveAsJson, Status.Active)]
            [InlineData(StatusJsonConverter.ClosedAsJson, Status.Closed)]
            public void Should_Return_Enum_Value_For_Valid_Json(string json, Status expected)
            {
                var enumValue = StatusJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_For_Invalid_Json()
            {
                var act = FluentActions.Invoking(() => StatusJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(Status.Active, StatusJsonConverter.ActiveAsJson)]
            [InlineData(Status.Closed, StatusJsonConverter.ClosedAsJson)]
            public void Should_Return_Json_For_Valid_Enum_Value(Status enumValue, string expected)
            {
                var json = StatusJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_For_Invalid_Enum_Value()
            {
                var act = FluentActions.Invoking(() => StatusJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}