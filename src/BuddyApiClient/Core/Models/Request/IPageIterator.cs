namespace BuddyApiClient.Core.Models.Request
{
    public interface IPageIterator
    {
        Task Iterate(CancellationToken cancellationToken = default);
    }
}