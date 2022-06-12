namespace BuddyApiClient.Test.PermissionSets.Models
{
    using BuddyApiClient.PermissionSets.Models;

    public sealed class TypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TypeJsonConverter.DeveloperAsJson, Type.Developer)]
            [InlineData(TypeJsonConverter.ReadOnlyAsJson, Type.ReadOnly)]
            [InlineData(TypeJsonConverter.CustomAsJson, Type.Custom)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, Type expected)
            {
                var enumValue = TypeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => TypeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}