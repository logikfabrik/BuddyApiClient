namespace BuddyApiClient.Core.Models.Request
{
    using BuddyApiClient.Core.Models.Response;
    using EnsureThat;

    public delegate Task<T2?> PageRequestHandler<in T1, T2>(T1 pageQuery, CancellationToken cancellationToken = default) where T1 : PageQuery where T2 : PageResponse;

    public delegate Task<bool> PageResponseHandler<in T1, in T2>(T1 pageQuery, T2? response, CancellationToken cancellationToken = default) where T1 : PageQuery where T2 : PageResponse;

    internal sealed class PageIterator<T1, T2> : IPageIterator where T1 : PageQuery where T2 : PageResponse
    {
        private readonly T1 _pageQuery;
        private readonly PageRequestHandler<T1, T2> _pageRequestHandler;
        private readonly PageResponseHandler<T1, T2> _pageResponseHandler;

        public PageIterator(PageRequestHandler<T1, T2> pageRequestHandler, PageResponseHandler<T1, T2> pageResponseHandler, T1 pageQuery)
        {
            _pageRequestHandler = Ensure.Any.HasValue(pageRequestHandler, nameof(pageRequestHandler));
            _pageResponseHandler = Ensure.Any.HasValue(pageResponseHandler, nameof(pageResponseHandler));
            _pageQuery = Ensure.Any.HasValue(pageQuery, nameof(pageQuery));

            _pageQuery.PageIndex ??= 1;
            _pageQuery.PageSize ??= 20;
        }

        public async Task Iterate(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                var response = await _pageRequestHandler(_pageQuery, cancellationToken);

                if (!await _pageResponseHandler(_pageQuery, response, cancellationToken))
                {
                    return;
                }

                if (response != null && response.Count < _pageQuery.PageSize)
                {
                    return;
                }

                _pageQuery.PageIndex++;
            }
        }
    }
}