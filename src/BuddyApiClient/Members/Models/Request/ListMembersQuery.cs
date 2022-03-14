namespace BuddyApiClient.Members.Models.Request
{
    using BuddyApiClient.Core.Models.Request;

    public sealed record ListMembersQuery : SortQuery
    {
        public SortMembersBy? SortBy { get; set; }

        protected override string? GetSortBy()
        {
            return SortBy switch
            {
                SortMembersBy.Email => "email",
                SortMembersBy.Name => "name",
                _ => null
            };
        }
    }
}