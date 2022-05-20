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
            public void Should_Return_Enum_If_Json_Is_Valid(string json, FilePlace expected)
            {
                var enumValue = FilePlaceJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Json_Is_Invalid()
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
            public void Should_Return_Json_If_Enum_Is_Valid(FilePlace enumValue, string expected)
            {
                var json = FilePlaceJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Enum_Is_Invalid()
            {
                var act = FluentActions.Invoking(() => FilePlaceJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}