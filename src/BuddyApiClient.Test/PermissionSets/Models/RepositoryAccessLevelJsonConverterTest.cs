namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using Shouldly;
    using Xunit;

    public sealed class RepositoryAccessLevelJsonConverterTest
    {
        [Theory]
        [InlineData(RepositoryAccessLevelJsonConverter.ReadOnlyAsJson, RepositoryAccessLevel.ReadOnly)]
        [InlineData(RepositoryAccessLevelJsonConverter.ReadWriteAsJson, RepositoryAccessLevel.ReadWrite)]
        public void ConvertFrom_For_Valid_Json_Should_Return_Enum_Value(string json, RepositoryAccessLevel expected)
        {
            var enumValue = RepositoryAccessLevelJsonConverter.ConvertFrom(json);

            enumValue.ShouldBe(expected);
        }

        [Fact]
        public void ConvertFrom_For_Invalid_Json_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => RepositoryAccessLevelJsonConverter.ConvertFrom(null));

            e.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(RepositoryAccessLevel.ReadOnly, RepositoryAccessLevelJsonConverter.ReadOnlyAsJson)]
        [InlineData(RepositoryAccessLevel.ReadWrite, RepositoryAccessLevelJsonConverter.ReadWriteAsJson)]
        public void ConvertTo_For_Valid_Enum_Value_Should_Return_Json(RepositoryAccessLevel enumValue, string expected)
        {
            var json = RepositoryAccessLevelJsonConverter.ConvertTo(enumValue);

            json.ShouldBe(expected);
        }

        [Fact]
        public void ConvertTo_For_Invalid_Enum_Value_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => RepositoryAccessLevelJsonConverter.ConvertTo(null));

            e.ShouldNotBeNull();
        }
    }
}