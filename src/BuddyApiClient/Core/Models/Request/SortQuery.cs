namespace BuddyApiClient.Core.Models.Request
{
    public abstract record SortQuery : PageQuery
    {
        public SortDirection? SortDirection { get; set; }

        protected abstract string? GetSortBy();

        private string? GetSortDirection()
        {
            return SortDirection switch
            {
                Request.SortDirection.Ascending => "ASC",
                Request.SortDirection.Descending => "DESC",
                _ => null
            };
        }

        private void AddSortBy(QueryStringParameters parameters)
        {
            parameters.Add("sort_by", GetSortBy);
        }

        private void AddSortDirection(QueryStringParameters parameters)
        {
            parameters.Add("sort_direction", GetSortDirection);
        }

        protected override void AddParameters(QueryStringParameters parameters)
        {
            base.AddParameters(parameters);

            AddSortBy(parameters);
            AddSortDirection(parameters);
        }
    }
}