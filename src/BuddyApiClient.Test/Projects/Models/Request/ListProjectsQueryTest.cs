namespace BuddyApiClient.Test.Projects.Models.Request
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Projects.Models.Request;

    public sealed class ListProjectsQueryTest
    {
        public sealed class Build
        {
            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToName()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.Name
                };

                sut.Build().Should().Be("?sort_by=name");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToNameAndSortDirectionIsSetToAscending()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.Name,
                    SortDirection = SortDirection.Ascending
                };

                sut.Build().Should().Be("?sort_by=name&sort_direction=ASC");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToCreateDate()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.CreateDate
                };

                sut.Build().Should().Be("?sort_by=create_date");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToCreateDateAndSortDirectionIsSetToDescending()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.CreateDate,
                    SortDirection = SortDirection.Descending
                };

                sut.Build().Should().Be("?sort_by=create_date&sort_direction=DESC");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToRepositorySize()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.RepositorySize
                };

                sut.Build().Should().Be("?sort_by=repository_size");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToRepositorySizeAndSortDirectionIsSetToAscending()
            {
                var sut = new ListProjectsQuery
                {
                    SortBy = SortProjectsBy.RepositorySize,
                    SortDirection = SortDirection.Ascending
                };

                sut.Build().Should().Be("?sort_by=repository_size&sort_direction=ASC");
            }
        }
    }
}
