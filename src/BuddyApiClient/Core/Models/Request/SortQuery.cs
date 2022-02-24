namespace BuddyApiClient.Core.Models.Request
{
    using System.Collections.Specialized;

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

        protected void AddSortBy(NameValueCollection parameters)
        {
            var sortBy = GetSortBy();

            if (sortBy == null)
            {
                return;
            }

            parameters.Add("sort_by", sortBy);
        }

        protected void AddSortDirection(NameValueCollection parameters)
        {
            var sortDirection = GetSortDirection();

            if (sortDirection == null)
            {
                return;
            }

            parameters.Add("sort_direction", sortDirection);
        }

        protected override void AddParameters(NameValueCollection parameters)
        {
            base.AddParameters(parameters);

            AddSortBy(parameters);
            AddSortDirection(parameters);
        }
    }
}