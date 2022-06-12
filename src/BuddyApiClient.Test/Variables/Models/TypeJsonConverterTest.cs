namespace BuddyApiClient.Test.Variables.Models
{
    using BuddyApiClient.Variables.Models;

    public sealed class TypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TypeJsonConverter.VariableAsJson, Type.Variable)]
            [InlineData(TypeJsonConverter.SshKeyAsJson, Type.SshKey)]
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

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(Type.Variable, TypeJsonConverter.VariableAsJson)]
            [InlineData(Type.SshKey, TypeJsonConverter.SshKeyAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(Type enumValue, string expected)
            {
                var json = TypeJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => TypeJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}