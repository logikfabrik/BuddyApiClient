namespace BuddyApiClient.Test.PermissionSets.Models
{
    using BuddyApiClient.PermissionSets.Models;

    public sealed class SandboxAccessLevelJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(SandboxAccessLevelJsonConverter.DeniedAsJson, SandboxAccessLevel.Denied)]
            [InlineData(SandboxAccessLevelJsonConverter.ReadOnlyAsJson, SandboxAccessLevel.ReadOnly)]
            [InlineData(SandboxAccessLevelJsonConverter.ReadWriteAsJson, SandboxAccessLevel.ReadWrite)]
            public void Should_ReturnEnum_When_JsonIsValid(string json, SandboxAccessLevel expected)
            {
                var enumValue = SandboxAccessLevelJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_JsonIsInvalid()
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
            public void Should_ReturnJson_When_EnumIsValid(SandboxAccessLevel enumValue, string expected)
            {
                var json = SandboxAccessLevelJsonConverter.ConvertTo(enumValue);

                json.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_When_EnumIsInvalid()
            {
                var act = FluentActions.Invoking(() => SandboxAccessLevelJsonConverter.ConvertTo(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}