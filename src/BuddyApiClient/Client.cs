namespace BuddyApiClient
{
    using BuddyApiClient.Core;
    using BuddyApiClient.CurrentUser;
    using BuddyApiClient.CurrentUserEmails;
    using BuddyApiClient.Workspaces;

    public sealed class Client : IClient
    {
        public Client(IHttpClientFactory httpClientFactory, string personalAccessToken)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClientFactory.CreateClient(), personalAccessToken));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public Client(HttpClient httpClient, string personalAccessToken)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClient, personalAccessToken));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IWorkspacesClient Workspaces { get; }
    }
}