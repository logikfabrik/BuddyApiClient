namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using Shouldly;
    using Xunit;

    public sealed class PipelineAccessLevelJsonConverterTest
    {
        [Theory]
        [InlineData(PipelineAccessLevelJsonConverter.DeniedAsJson, PipelineAccessLevel.Denied)]
        [InlineData(PipelineAccessLevelJsonConverter.ReadOnlyAsJson, PipelineAccessLevel.ReadOnly)]
        [InlineData(PipelineAccessLevelJsonConverter.RunOnlyAsJson, PipelineAccessLevel.RunOnly)]
        [InlineData(PipelineAccessLevelJsonConverter.ReadWriteAsJson, PipelineAccessLevel.ReadWrite)]
        public void ConvertFrom_For_Valid_Json_Should_Return_Enum_Value(string json, PipelineAccessLevel expected)
        {
            var enumValue = PipelineAccessLevelJsonConverter.ConvertFrom(json);

            enumValue.ShouldBe(expected);
        }

        [Fact]
        public void ConvertFrom_For_Invalid_Json_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => PipelineAccessLevelJsonConverter.ConvertFrom(null));

            e.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(PipelineAccessLevel.Denied, PipelineAccessLevelJsonConverter.DeniedAsJson)]
        [InlineData(PipelineAccessLevel.ReadOnly, PipelineAccessLevelJsonConverter.ReadOnlyAsJson)]
        [InlineData(PipelineAccessLevel.RunOnly, PipelineAccessLevelJsonConverter.RunOnlyAsJson)]
        [InlineData(PipelineAccessLevel.ReadWrite, PipelineAccessLevelJsonConverter.ReadWriteAsJson)]
        public void ConvertTo_For_Valid_Enum_Value_Should_Return_Json(PipelineAccessLevel enumValue, string expected)
        {
            var json = PipelineAccessLevelJsonConverter.ConvertTo(enumValue);

            json.ShouldBe(expected);
        }

        [Fact]
        public void ConvertTo_For_Invalid_Enum_Value_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => PipelineAccessLevelJsonConverter.ConvertTo(null));

            e.ShouldNotBeNull();
        }
    }
}