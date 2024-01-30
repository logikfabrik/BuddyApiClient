namespace BuddyApiClient.Test.Members.Models.Request
{
    using BuddyApiClient.Core.Models.Request;
    using BuddyApiClient.Members.Models.Request;

    public sealed class ListMembersQueryTest
    {
        public sealed class Build
        {
            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToName()
            {
                var sut = new ListMembersQuery
                {
                    SortBy = SortMembersBy.Name
                };

                sut.Build().Should().Be("?sort_by=name");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToNameAndSortDirectionIsSetToAscending()
            {
                var sut = new ListMembersQuery
                {
                    SortBy = SortMembersBy.Name,
                    SortDirection = SortDirection.Ascending
                };

                sut.Build().Should().Be("?sort_by=name&sort_direction=ASC");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToEmail()
            {
                var sut = new ListMembersQuery
                {
                    SortBy = SortMembersBy.Email
                };

                sut.Build().Should().Be("?sort_by=email");
            }

            [Fact]
            public void Should_ReturnQuery_When_SortByIsSetToEmailAndSortDirectionIsSetToDescending()
            {
                var sut = new ListMembersQuery
                {
                    SortBy = SortMembersBy.Email,
                    SortDirection = SortDirection.Descending
                };

                sut.Build().Should().Be("?sort_by=email&sort_direction=DESC");
            }
        }
    }
}
