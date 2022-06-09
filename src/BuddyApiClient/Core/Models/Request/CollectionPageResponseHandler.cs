namespace BuddyApiClient.Core.Models.Request
{
    using BuddyApiClient.Core.Models.Response;

    public delegate Task<bool> CollectionPageResponseHandler<in T1, in T2>(T1 collectionQuery, T2? response, CancellationToken cancellationToken = default) where T1 : CollectionQuery where T2 : CollectionPageResponse;
}