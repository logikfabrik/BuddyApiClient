namespace BuddyApiClient.Core.Models.Request
{
    public interface ICollectionIterator
    {
        Task Iterate(CancellationToken cancellationToken = default);
    }
}