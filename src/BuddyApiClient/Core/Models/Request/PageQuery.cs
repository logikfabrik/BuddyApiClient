namespace BuddyApiClient.Core.Models.Request
{
    using EnsureThat;

    public abstract record PageQuery : Query
    {
        private int? _pageIndex;
        private int? _pageSize;

        public int? PageIndex
        {
            get => _pageIndex;
            set
            {
                if (value is null)
                {
                    _pageIndex = null;

                    return;
                }

                _pageIndex = Ensure.Comparable.IsGt(value.Value, 0);
            }
        }

        public int? PageSize
        {
            get => _pageSize;
            set
            {
                if (value is null)
                {
                    _pageSize = null;

                    return;
                }

                _pageSize = Ensure.Comparable.IsGt(value.Value, 0);
            }
        }

        private string? GetPageIndex()
        {
            return _pageIndex?.ToString();
        }

        private string? GetPageSize()
        {
            return _pageSize?.ToString();
        }

        private void AddPageIndex(QueryStringParameters parameters)
        {
            parameters.Add("page", GetPageIndex);
        }

        private void AddPageSize(QueryStringParameters parameters)
        {
            parameters.Add("per_page", GetPageSize);
        }

        protected override void AddParameters(QueryStringParameters parameters)
        {
            AddPageIndex(parameters);
            AddPageSize(parameters);
        }
    }
}