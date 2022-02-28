namespace BuddyApiClient.Core
{
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using System.Text;
    using EnsureThat;

    internal static class HttpClientFacadeFactory
    {
        public static HttpClientFacade Create(HttpClient httpClient, Uri? baseUrl, string accessToken)
        {
            Ensure.Any.HasValue(httpClient, nameof(httpClient));
            Ensure.String.IsNotNullOrWhiteSpace(accessToken, nameof(accessToken));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            return Create(httpClient, baseUrl);
        }

        public static HttpClientFacade Create(HttpClient httpClient, Uri? baseUrl, string basicAuthClientId, string basicAuthClientSecret)
        {
            Ensure.Any.HasValue(httpClient, nameof(httpClient));
            Ensure.String.IsNotNullOrWhiteSpace(basicAuthClientId, nameof(basicAuthClientId));
            Ensure.String.IsNotNullOrWhiteSpace(basicAuthClientSecret, nameof(basicAuthClientSecret));

            var parameter = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{basicAuthClientId}:{basicAuthClientSecret}"));

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", parameter);

            return Create(httpClient, baseUrl);
        }

        private static HttpClientFacade Create(HttpClient httpClient, Uri? baseUrl)
        {
            httpClient.BaseAddress = baseUrl;

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BuddyApiClient", null));
            httpClient.DefaultRequestHeaders.Add("X-Buddy-Media-Type", "buddy.v1.1.0");

            return new HttpClientFacade(httpClient);
        }
    }
}