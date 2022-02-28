namespace BuddyApiClient.Projects.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListProjectsQuery : SortQuery
    {
        public SortProjectsBy? SortBy { get; set; }

        protected override string? GetSortBy()
        {
            if (!SortBy.HasValue)
            {
                return null;
            }

            throw new NotImplementedException();
        }
    }
}