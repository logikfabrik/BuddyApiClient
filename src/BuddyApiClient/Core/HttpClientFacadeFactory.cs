namespace BuddyApiClient.Core
{
    using System.Net.Http.Headers;
    using System.Net.Mime;

    internal static class HttpClientFacadeFactory
    {
        public static HttpClientFacade Create(string accessToken, Uri baseUrl, HttpClient httpClient)
        {
            ArgumentNullException.ThrowIfNull(httpClient);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BuddyApiClient", null));
            httpClient.DefaultRequestHeaders.Add("X-Buddy-Media-Type", "buddy.v1.1.0");

            return new HttpClientFacade(accessToken, baseUrl, httpClient);
        }
    }
}