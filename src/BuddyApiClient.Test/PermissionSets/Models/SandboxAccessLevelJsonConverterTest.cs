namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class SandboxAccessLevelJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(SandboxAccessLevelJsonConverter.DeniedAsJson, SandboxAccessLevel.Denied)]
            [InlineData(SandboxAccessLevelJsonConverter.ReadOnlyAsJson, SandboxAccessLevel.ReadOnly)]
            [InlineData(SandboxAccessLevelJsonConverter.ReadWriteAsJson, SandboxAccessLevel.ReadWrite)]
            public void Should_Return_Enum_If_Json_Is_Valid(string json, SandboxAccessLevel expected)
            {
                var enumValue = SandboxAccessLevelJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Json_Is_Invalid()
            {
                var act = FluentActions.Invoking(() => SandboxAccessLevelJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(SandboxAccessLevel.Denied, SandboxAccessLevelJsonConverter.DeniedAsJson)]
            [InlineData(SandboxAccessLevel.ReadOnly, SandboxAccessLevelJsonConverter.ReadOnlyAsJson)]
            [InlineData(SandboxAccessLevel.ReadWrite, SandboxAccessLevelJsonConverter.ReadWriteAsJson)]
            public void Should_Return_Json_If_Enum_Is_Valid(SandboxAccessLevel enumValue, string expected)
            {
                var json = SandboxAccessLevelJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Enum_Is_Invalid()
            {
                var act = FluentActions.Invoking(() => SandboxAccessLevelJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}