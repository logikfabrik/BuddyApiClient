namespace BuddyApiClient.CurrentUser
{
    using BuddyApiClient.CurrentUser.Models.Request;
    using BuddyApiClient.CurrentUser.Models.Response;

    public interface ICurrentUserClient
    {
        Task<CurrentUserDetails?> Get(CancellationToken cancellationToken = default);

        Task<CurrentUserDetails?> Update(UpdateUser content, CancellationToken cancellationToken = default);
    }
}