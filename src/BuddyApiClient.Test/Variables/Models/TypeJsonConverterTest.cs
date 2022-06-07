namespace BuddyApiClient.Test.Variables.Models
{
    using System;
    using BuddyApiClient.Variables.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class TypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TypeJsonConverter.VariableAsJson, BuddyApiClient.Variables.Models.Type.Variable)]
            [InlineData(TypeJsonConverter.SshKeyAsJson, BuddyApiClient.Variables.Models.Type.SshKey)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, BuddyApiClient.Variables.Models.Type expected)
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
            [InlineData(BuddyApiClient.Variables.Models.Type.Variable, TypeJsonConverter.VariableAsJson)]
            [InlineData(BuddyApiClient.Variables.Models.Type.SshKey, TypeJsonConverter.SshKeyAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(BuddyApiClient.Variables.Models.Type enumValue, string expected)
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