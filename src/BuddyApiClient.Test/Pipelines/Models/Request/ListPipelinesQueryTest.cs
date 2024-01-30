namespace BuddyApiClient.Test.Pipelines.Models.Request
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Pipelines.Models.Request;

    public sealed class ListPipelinesQueryTest
    {
        public sealed class Build
        {
            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToName()
            {
                var sut = new ListPipelinesQuery
                {
                    SortBy = SortPipelinesBy.Name
                };

                sut.Build().Should().Be("?sort_by=name");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToNameAndSortDirectionIsSetToAscending()
            {
                var sut = new ListPipelinesQuery
                {
                    SortBy = SortPipelinesBy.Name,
                    SortDirection = SortDirection.Ascending
                };

                sut.Build().Should().Be("?sort_by=name&sort_direction=ASC");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToIdentifier()
            {
                var sut = new ListPipelinesQuery
                {
                    SortBy = SortPipelinesBy.Identifier
                };

                sut.Build().Should().Be("?sort_by=id");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToIdentifierAndSortDirectionIsSetToDescending()
            {
                var sut = new ListPipelinesQuery
                {
                    SortBy = SortPipelinesBy.Identifier,
                    SortDirection = SortDirection.Descending
                };

                sut.Build().Should().Be("?sort_by=id&sort_direction=DESC");
            }
        }
    }
}
