namespace BuddyApiClient.Core.Models.Request
{
    using BuddyApiClient.Core.Models.Response;
    using EnsureThat;

    internal static class CollectionIterator
    {
        public const int DefaultPageIndex = 1;
        public const int DefaultPageSize = 20;
    }

    internal sealed class CollectionIterator<T1, T2> : ICollectionIterator where T1 : CollectionQuery where T2 : CollectionPageResponse
    {
        private readonly CollectionPageRequestHandler<T1, T2> _collectionPageRequestHandler;
        private readonly CollectionPageResponseHandler<T1, T2> _collectionPageResponseHandler;
        private readonly T1 _collectionQuery;

        public CollectionIterator(CollectionPageRequestHandler<T1, T2> collectionPageRequestHandler, CollectionPageResponseHandler<T1, T2> collectionPageResponseHandler, T1 collectionQuery)
        {
            _collectionPageRequestHandler = Ensure.Any.HasValue(collectionPageRequestHandler, nameof(collectionPageRequestHandler));
            _collectionPageResponseHandler = Ensure.Any.HasValue(collectionPageResponseHandler, nameof(collectionPageResponseHandler));
            _collectionQuery = Ensure.Any.HasValue(collectionQuery, nameof(collectionQuery));

            _collectionQuery.PageIndex ??= CollectionIterator.DefaultPageIndex;
            _collectionQuery.PageSize ??= CollectionIterator.DefaultPageSize;
        }

        public async Task Iterate(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var response = await _collectionPageRequestHandler(_collectionQuery, cancellationToken);

                if (!await _collectionPageResponseHandler(_collectionQuery, response, cancellationToken))
                {
                    return;
                }

                if (response is not null && response.Count < _collectionQuery.PageSize)
                {
                    return;
                }

                _collectionQuery.PageIndex++;
            }
        }
    }
}