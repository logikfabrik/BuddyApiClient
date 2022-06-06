namespace BuddyApiClient.Test.Core.Models.Request
{
    using System;
    using FluentAssertions;
    using Xunit;

    public sealed class PageQueryTest
    {
        public sealed class PageIndex
        {
            [Fact]
            public void Should_Throw_When_SetToLessThanZero()
            {
                var sut = new PageQuery();

                const int pageIndex = -1;

                var act = FluentActions.Invoking(() => sut.PageIndex = pageIndex);

                act.Should().Throw<ArgumentOutOfRangeException>();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(10)]
            public void Should_NotThrow_When_SetToZeroOrGreater(int pageIndex)
            {
                var sut = new PageQuery();

                var act = FluentActions.Invoking(() => sut.PageIndex = pageIndex);

                act.Should().NotThrow<ArgumentOutOfRangeException>();
            }
        }

        public sealed class PageSize
        {
            [Theory]
            [InlineData(0)]
            [InlineData(-10)]
            public void Should_Throw_When_SetToZeroOrLess(int pageSize)
            {
                var sut = new PageQuery();

                var act = FluentActions.Invoking(() => sut.PageSize = pageSize);

                act.Should().Throw<ArgumentOutOfRangeException>();
            }

            [Fact]
            public void Should_NotThrow_When_SetToGreaterThanZero()
            {
                var sut = new PageQuery();

                const int pageSize = 1;

                var act = FluentActions.Invoking(() => sut.PageSize = pageSize);

                act.Should().NotThrow<ArgumentOutOfRangeException>();
            }
        }

        public sealed class Build
        {
            [Theory]
            [InlineData(10, "?page=10")]
            public void Should_ReturnQuery_When_PageIndexIsSet(int pageIndex, string expected)
            {
                var sut = new PageQuery
                {
                    PageIndex = pageIndex
                };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }

            [Theory]
            [InlineData(10, "?per_page=10")]
            public void Should_ReturnQuery_When_PageSizeIsSet(int pageSize, string expected)
            {
                var sut = new PageQuery
                {
                    PageSize = pageSize
                };

                var actual = sut.Build();

                actual.Should().Be(expected);
            }
        }

        private sealed record PageQuery : BuddyApiClient.Core.Models.Request.PageQuery;
    }
}