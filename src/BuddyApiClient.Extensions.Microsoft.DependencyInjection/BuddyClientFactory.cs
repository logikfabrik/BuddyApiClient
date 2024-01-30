using Microsoft.Extensions.Options;

namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    public sealed class BuddyClientFactory : IBuddyClientFactory
    {
        private readonly HttpClient _httpClient;
        private readonly Uri? _baseUrl;

        public BuddyClientFactory(HttpClient httpClient, IOptions<BuddyClientOptions> options)
        {
            _httpClient = httpClient;
            _baseUrl = options.Value.BaseUrl;
        }

        public IBuddyClient Create(string accessToken)
        {
            return new BuddyClient(accessToken, _httpClient, _baseUrl);
        }
    }
}