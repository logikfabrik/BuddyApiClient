namespace BuddyApiClient.Test.Variables.Models
{
    using BuddyApiClient.Variables.Models;

    public sealed class FilePlaceJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(FilePlaceJsonConverter.NoneAsJson, FilePlace.None)]
            [InlineData(FilePlaceJsonConverter.ContainerAsJson, FilePlace.Container)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, FilePlace expected)
            {
                var enumValue = FilePlaceJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
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
            public void Should_ReturnJson_When_EnumIsValid(FilePlace enumValue, string expected)
            {
                var json = FilePlaceJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => FilePlaceJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}