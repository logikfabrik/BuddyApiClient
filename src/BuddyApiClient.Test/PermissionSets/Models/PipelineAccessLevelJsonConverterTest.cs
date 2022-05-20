namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class PipelineAccessLevelJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(PipelineAccessLevelJsonConverter.DeniedAsJson, PipelineAccessLevel.Denied)]
            [InlineData(PipelineAccessLevelJsonConverter.ReadOnlyAsJson, PipelineAccessLevel.ReadOnly)]
            [InlineData(PipelineAccessLevelJsonConverter.RunOnlyAsJson, PipelineAccessLevel.RunOnly)]
            [InlineData(PipelineAccessLevelJsonConverter.ReadWriteAsJson, PipelineAccessLevel.ReadWrite)]
            public void Should_Return_Enum_If_Json_Is_Valid(string json, PipelineAccessLevel expected)
            {
                var enumValue = PipelineAccessLevelJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Json_Is_Invalid()
            {
                var act = FluentActions.Invoking(() => PipelineAccessLevelJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }

        public sealed class ConvertTo
        {
            [Theory]
            [InlineData(PipelineAccessLevel.Denied, PipelineAccessLevelJsonConverter.DeniedAsJson)]
            [InlineData(PipelineAccessLevel.ReadOnly, PipelineAccessLevelJsonConverter.ReadOnlyAsJson)]
            [InlineData(PipelineAccessLevel.RunOnly, PipelineAccessLevelJsonConverter.RunOnlyAsJson)]
            [InlineData(PipelineAccessLevel.ReadWrite, PipelineAccessLevelJsonConverter.ReadWriteAsJson)]
            public void Should_Return_Json_If_Enum_Is_Valid(PipelineAccessLevel enumValue, string expected)
            {
                var json = PipelineAccessLevelJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_If_Enum_Is_Invalid()
            {
                var act = FluentActions.Invoking(() => PipelineAccessLevelJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}