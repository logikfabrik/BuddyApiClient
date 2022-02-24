namespace BuddyApiClient.Core.Models.Response
{
    public abstract record PageResponse : Response
    {
        public abstract int Count { get; }
    }
}