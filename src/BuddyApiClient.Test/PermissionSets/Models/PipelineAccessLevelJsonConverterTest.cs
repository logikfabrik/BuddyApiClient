namespace BuddyApiClient.Test.PermissionSets.Models
{
    using BuddyApiClient.PermissionSets.Models;

    public sealed class PipelineAccessLevelJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(PipelineAccessLevelJsonConverter.DeniedAsJson, PipelineAccessLevel.Denied)]
            [InlineData(PipelineAccessLevelJsonConverter.ReadOnlyAsJson, PipelineAccessLevel.ReadOnly)]
            [InlineData(PipelineAccessLevelJsonConverter.RunOnlyAsJson, PipelineAccessLevel.RunOnly)]
            [InlineData(PipelineAccessLevelJsonConverter.ReadWriteAsJson, PipelineAccessLevel.ReadWrite)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, PipelineAccessLevel expected)
            {
                var enumValue = PipelineAccessLevelJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
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
            public void Should_ReturnJson_When_EnumIsValid(PipelineAccessLevel enumValue, string expected)
            {
                var json = PipelineAccessLevelJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => PipelineAccessLevelJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}