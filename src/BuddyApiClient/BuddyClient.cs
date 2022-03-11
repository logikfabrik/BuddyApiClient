namespace BuddyApiClient
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.Members;
    using BuddyApiClient.PermissionSets;
    using BuddyApiClient.Workspaces;
    using Microsoft.Extensions.Options;

    public sealed class BuddyClient : IBuddyClient
    {
        public BuddyClient(IHttpClientFactory httpClientFactory, IOptions<BuddyClientOptions> options)
            : this(() => HttpClientFacadeFactory.Create(httpClientFactory.CreateClient(), options.Value.BaseUrl, options.Value.AccessToken))
        {
        }

        public BuddyClient(HttpClient httpClient, IOptions<BuddyClientOptions> options)
            : this(() => HttpClientFacadeFactory.Create(httpClient, options.Value.BaseUrl, options.Value.AccessToken))
        {
        }

        private BuddyClient(Func<HttpClientFacade> factory)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(factory);

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Members = new MembersClient(httpClientFacade);
            PermissionSets = new PermissionSetsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IMembersClient Members { get; }

        public IPermissionSetsClient PermissionSets { get; }

        public IWorkspacesClient Workspaces { get; }
    }
}