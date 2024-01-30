namespace BuddyApiClient.Pipelines.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListPipelinesQuery : SortQuery
    {
        public SortPipelinesBy? SortBy { get; set; }

        protected override string? GetSortBy()
        {
            return SortBy switch
            {
                SortPipelinesBy.Name => "name",
                SortPipelinesBy.Identifier => "id",
                _ => null
            };
        }
    }
}