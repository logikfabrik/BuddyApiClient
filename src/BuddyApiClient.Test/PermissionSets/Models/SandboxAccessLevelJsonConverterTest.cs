namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using Shouldly;
    using Xunit;

    public sealed class SandboxAccessLevelJsonConverterTest
    {
        [Theory]
        [InlineData(SandboxAccessLevelJsonConverter.DeniedAsJson, SandboxAccessLevel.Denied)]
        [InlineData(SandboxAccessLevelJsonConverter.ReadOnlyAsJson, SandboxAccessLevel.ReadOnly)]
        [InlineData(SandboxAccessLevelJsonConverter.ReadWriteAsJson, SandboxAccessLevel.ReadWrite)]
        public void ConvertFrom_For_Valid_Json_Should_Return_Enum_Value(string json, SandboxAccessLevel expected)
        {
            var enumValue = SandboxAccessLevelJsonConverter.ConvertFrom(json);

            enumValue.ShouldBe(expected);
        }

        [Fact]
        public void ConvertFrom_For_Invalid_Json_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => SandboxAccessLevelJsonConverter.ConvertFrom(null));

            e.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(SandboxAccessLevel.Denied, SandboxAccessLevelJsonConverter.DeniedAsJson)]
        [InlineData(SandboxAccessLevel.ReadOnly, SandboxAccessLevelJsonConverter.ReadOnlyAsJson)]
        [InlineData(SandboxAccessLevel.ReadWrite, SandboxAccessLevelJsonConverter.ReadWriteAsJson)]
        public void ConvertTo_For_Valid_Enum_Value_Should_Return_Json(SandboxAccessLevel enumValue, string expected)
        {
            var json = SandboxAccessLevelJsonConverter.ConvertTo(enumValue);

            json.ShouldBe(expected);
        }

        [Fact]
        public void ConvertTo_For_Invalid_Enum_Value_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => SandboxAccessLevelJsonConverter.ConvertTo(null));

            e.ShouldNotBeNull();
        }
    }
}