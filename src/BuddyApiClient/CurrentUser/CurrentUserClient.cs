namespace BuddyApiClient.CurrentUser
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser.Models.Request;
    using BuddyApiClient.CurrentUser.Models.Response;
    using EnsureThat;

    internal sealed class CurrentUserClient : ClientBase, ICurrentUserClient
    {
        public CurrentUserClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<CurrentUserDetails?> Get(CancellationToken cancellationToken = default)
        {
            return await HttpClientFacade.Get<CurrentUserDetails>("user", cancellationToken);
        }

        public async Task<CurrentUserDetails?> Update(UpdateUser content, CancellationToken cancellationToken = default)
        {
            return await HttpClientFacade.Patch<CurrentUserDetails>("user", Ensure.Any.HasValue(content, nameof(content)), cancellationToken);
        }
    }
}