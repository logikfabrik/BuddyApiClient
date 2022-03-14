namespace BuddyApiClient.Test.Projects.Models
{
    using System;
    using BuddyApiClient.Projects.Models;
    using Shouldly;
    using Xunit;

    public sealed class StatusJsonConverterTest
    {
        [Theory]
        [InlineData(StatusJsonConverter.ActiveAsJson, Status.Active)]
        [InlineData(StatusJsonConverter.ClosedAsJson, Status.Closed)]
        public void ConvertFrom_For_Valid_Json_Should_Return_Enum_Value(string json, Status expected)
        {
            var enumValue = StatusJsonConverter.ConvertFrom(json);

            enumValue.ShouldBe(expected);
        }

        [Fact]
        public void ConvertFrom_For_Invalid_Json_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => StatusJsonConverter.ConvertFrom(null));

            e.ShouldNotBeNull();
        }

        [Theory]
        [InlineData(Status.Active, StatusJsonConverter.ActiveAsJson)]
        [InlineData(Status.Closed, StatusJsonConverter.ClosedAsJson)]
        public void ConvertTo_For_Valid_Enum_Value_Should_Return_Json(Status enumValue, string expected)
        {
            var json = StatusJsonConverter.ConvertTo(enumValue);

            json.ShouldBe(expected);
        }

        [Fact]
        public void ConvertTo_For_Invalid_Enum_Value_Should_Throw()
        {
            var e = Assert.Throws<NotSupportedException>(() => StatusJsonConverter.ConvertTo(null));

            e.ShouldNotBeNull();
        }
    }
}