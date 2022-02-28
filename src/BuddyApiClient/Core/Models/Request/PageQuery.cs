namespace BuddyApiClient.Core.Models.Request
{
    using System.Collections.Specialized;
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

        protected void AddPageIndex(NameValueCollection parameters)
        {
            if (_pageIndex is null)
            {
                return;
            }

            parameters.Add("page", _pageIndex.ToString());
        }

        protected void AddPageSize(NameValueCollection parameters)
        {
            if (_pageSize is null)
            {
                return;
            }

            parameters.Add("per_page", _pageSize.ToString());
        }

        protected override void AddParameters(NameValueCollection parameters)
        {
            AddPageIndex(parameters);
            AddPageSize(parameters);
        }
    }
}