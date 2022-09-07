namespace BuddyApiClient.CurrentUser
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser.Models.Request;
    using BuddyApiClient.CurrentUser.Models.Response;

    internal sealed class CurrentUserClient : ClientBase, ICurrentUserClient
    {
        public CurrentUserClient(Lazy<HttpClientFacade> httpClientFacade) : base(httpClientFacade)
        {
        }

        public async Task<CurrentUserDetails?> Get(CancellationToken cancellationToken = default)
        {
            const string url = "user";

            return await HttpClientFacade.Get<CurrentUserDetails>(url, cancellationToken: cancellationToken);
        }

        public async Task<CurrentUserDetails?> Update(UpdateUser content, CancellationToken cancellationToken = default)
        {
            const string url = "user";

            return await HttpClientFacade.Patch<CurrentUserDetails>(url, content, cancellationToken: cancellationToken);
        }
    }
}