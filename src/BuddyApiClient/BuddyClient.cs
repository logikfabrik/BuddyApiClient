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
            var httpClientFacade = new Lazy<HttpClientFacade>(() => Create(httpClientFactory.CreateClient(), options.Value));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public BuddyClient(HttpClient httpClient, IOptions<BuddyClientOptions> options)
        {
            var httpClientFacade = new Lazy<HttpClientFacade>(() => Create(httpClient, options.Value));

            CurrentUser = new CurrentUserClient(httpClientFacade);
            CurrentUserEmails = new CurrentUserEmailsClient(httpClientFacade);
            Workspaces = new WorkspacesClient(httpClientFacade);
        }

        public ICurrentUserClient CurrentUser { get; }

        public ICurrentUserEmailsClient CurrentUserEmails { get; }

        public IWorkspacesClient Workspaces { get; }

        private static HttpClientFacade Create(HttpClient httpClient, BuddyClientOptions options)
        {
            if (options.UseAccessToken)
            {
                return HttpClientFacadeFactory.Create(httpClient, options.BaseUrl, options.AccessToken!);
            }

            if (options.UseBasicAuth)
            {
                return HttpClientFacadeFactory.Create(httpClient, options.BaseUrl, options.BasicAuthClientId!, options.BasicAuthClientSecret!);
            }

            throw new NotSupportedException();
        }
    }
}