﻿namespace BuddyApiClient.Test.Core.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed class SortQueryTest
    {
        public sealed class Build
        {
            [Theory]
            [InlineData(SortDirection.Ascending, "?sort_direction=ASC")]
            [InlineData(SortDirection.Descending, "?sort_direction=DESC")]
            public void Should_ReturnQuery_When_SortDirectionIsSet(SortDirection sortDirection, string expected)
            {
                var sut = new SortQuery
                {
                    SortDirection = sortDirection
                };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }
        }

        private sealed record SortQuery : BuddyApiClient.Core.Models.Request.SortQuery
        {
            protected override string? GetSortBy()
            {
                return null;
            }
        }
    }
}