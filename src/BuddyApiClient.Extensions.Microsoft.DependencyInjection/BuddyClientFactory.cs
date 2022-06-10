namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    using EnsureThat;

    public sealed class BuddyClientFactory : IBuddyClientFactory
    {
        private readonly HttpClient _httpClient;

        public BuddyClientFactory(HttpClient httpClient)
        {
            _httpClient = Ensure.Any.HasValue(httpClient, nameof(httpClient));
        }

        public IBuddyClient Create(string accessToken, Uri? baseUrl = null)
        {
            return new BuddyClient(accessToken, baseUrl, _httpClient);
        }
    }
}