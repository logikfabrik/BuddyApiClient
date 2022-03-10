namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using Shouldly;
    using Xunit;

    public sealed class TypeJsonConverterTest
    {
        [Theory]
        [InlineData(TypeJsonConverter.DeveloperAsJson, BuddyApiClient.PermissionSets.Models.Type.Developer)]
        [InlineData(TypeJsonConverter.ReadOnlyAsJson, BuddyApiClient.PermissionSets.Models.Type.ReadOnly)]
        [InlineData(TypeJsonConverter.CustomAsJson, BuddyApiClient.PermissionSets.Models.Type.Custom)]
        public void ConvertFrom_For_Valid_Json_Should_Return_Enum_Value(string json, BuddyApiClient.PermissionSets.Models.Type expected)
        {
            var enumValue = TypeJsonConverter.ConvertFrom(json);

            enumValue.ShouldBe(expected);
        }

        [Fact]
        public void ConvertFrom_For_Invalid_Json_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => TypeJsonConverter.ConvertFrom(null));

            e.ShouldNotBeNull();
        }
    }
}