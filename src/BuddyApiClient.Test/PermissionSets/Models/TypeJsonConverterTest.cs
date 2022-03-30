﻿namespace BuddyApiClient.Test.PermissionSets.Models
{
    using System;
    using BuddyApiClient.PermissionSets.Models;
    using FluentAssertions;
    using Xunit;

    public sealed class TypeJsonConverterTest
    {
        public sealed class ConvertFrom
        {
            [Theory]
            [InlineData(TypeJsonConverter.DeveloperAsJson, BuddyApiClient.PermissionSets.Models.Type.Developer)]
            [InlineData(TypeJsonConverter.ReadOnlyAsJson, BuddyApiClient.PermissionSets.Models.Type.ReadOnly)]
            [InlineData(TypeJsonConverter.CustomAsJson, BuddyApiClient.PermissionSets.Models.Type.Custom)]
            public void Should_Return_Enum_Value_For_Valid_Json(string json, BuddyApiClient.PermissionSets.Models.Type expected)
            {
                var enumValue = TypeJsonConverter.ConvertFrom(json);

                enumValue.Should().Be(expected);
            }

            [Fact]
            public void Should_Throw_For_Invalid_Json()
            {
                var act = FluentActions.Invoking(() => TypeJsonConverter.ConvertFrom(null));

                act.Should().Throw<NotSupportedException>();
            }
        }
    }
}