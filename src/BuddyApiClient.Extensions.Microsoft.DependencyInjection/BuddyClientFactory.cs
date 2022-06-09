namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    using EnsureThat;
    using global::Microsoft.Extensions.Options;

    public sealed class BuddyClientFactory : IBuddyClientFactory
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<BuddyClientOptions> _options;

        public BuddyClientFactory(IOptions<BuddyClientOptions> options, HttpClient httpClient)
        {
            Ensure.Any.HasValue(options, nameof(options));
            Ensure.Any.HasValue(httpClient, nameof(httpClient));

            _options = options;
            _httpClient = httpClient;
        }

        public IBuddyClient Create(string accessToken)
        {
            Ensure.String.IsNotNullOrEmpty(accessToken, nameof(accessToken));

            return new BuddyClient(_options.Value.BaseUrl, accessToken, _httpClient);
        }
    }
}