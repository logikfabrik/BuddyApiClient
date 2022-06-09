namespace BuddyApiClient.Core.Models.Request
{
    using BuddyApiClient.Core.Models.Response;

    public delegate Task<T2?> CollectionPageRequestHandler<in T1, T2>(T1 collectionQuery, CancellationToken cancellationToken = default) where T1 : CollectionQuery where T2 : CollectionPageResponse;
}