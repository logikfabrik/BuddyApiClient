namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class RepositoryAccessLevelJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(RepositoryAccessLevelJsonConverter.ReadOnlyAsJson, RepositoryAccessLevel.ReadOnly)]
            [InlineData(RepositoryAccessLevelJsonConverter.ReadWriteAsJson, RepositoryAccessLevel.ReadWrite)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, RepositoryAccessLevel expected)
            {
                var enumValue = RepositoryAccessLevelJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
            {
                var act = FluentActions.Invoking(() => RepositoryAccessLevelJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(RepositoryAccessLevel.ReadOnly, RepositoryAccessLevelJsonConverter.ReadOnlyAsJson)]
            [InlineData(RepositoryAccessLevel.ReadWrite, RepositoryAccessLevelJsonConverter.ReadWriteAsJson)]
            public void Should_ReturnJson_When_EnumIsValid(RepositoryAccessLevel enumValue, string expected)
            {
                var json = RepositoryAccessLevelJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => RepositoryAccessLevelJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}