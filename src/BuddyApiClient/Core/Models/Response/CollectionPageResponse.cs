namespace BuddyApiClient.Core.Models.Response
{
    public abstract record CollectionPageResponse : CollectionResponse
    {
        public abstract int Count { get; }
    }
}