namespace BuddyApiClient
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.Workspaces;
    using Microsoft.Extensions.Options;

    public sealed class BuddyClient : IBuddyClient
    {
        public BuddyClient(IHttpClientFactory httpClientFactory, IOptions<BuddyClientOptions> options)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClientFactory.CreateClient(), options.Value.BaseUrl, options.Value.AccessToken!));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public BuddyClient(HttpClient httpClient, IOptions<BuddyClientOptions> options)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClient, options.Value.BaseUrl, options.Value.AccessToken!));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IWorkspacesClient Workspaces { get; }
    }
}