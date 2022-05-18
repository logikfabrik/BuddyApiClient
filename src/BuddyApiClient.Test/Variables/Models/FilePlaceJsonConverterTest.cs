namespace BuddyApiClient.Test.Variables.Models
{
    using System;
    using BuddyApiClient.Variables.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class FilePlaceJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(FilePlaceJsonConverter.NoneAsJson, FilePlace.None)]
            [InlineData(FilePlaceJsonConverter.ContainerAsJson, FilePlace.Container)]
            public void Should_Return_Enum_Value_For_Valid_Json(string json, FilePlace expected)
            {
                var enumValue = FilePlaceJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_For_Invalid_Json()
            {
                var act = FluentActions.Invoking(() => FilePlaceJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(FilePlace.None, FilePlaceJsonConverter.NoneAsJson)]
            [InlineData(FilePlace.Container, FilePlaceJsonConverter.ContainerAsJson)]
            public void Should_Return_Json_For_Valid_Enum_Value(FilePlace enumValue, string expected)
            {
                var json = FilePlaceJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_For_Invalid_Enum_Value()
            {
                var act = FluentActions.Invoking(() => FilePlaceJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}