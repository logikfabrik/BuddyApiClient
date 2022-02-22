namespace BuddyApiClient.Core
{
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using EnsureThat;

    internal static class HttpClientFacadeFactory
    {
        public static HttpClientFacade Create(HttpClient httpClient, string personalAccessToken)
        {
            Ensure.Any.HasValue(httpClient, nameof(httpClient));
            Ensure.String.IsNotNullOrWhiteSpace(personalAccessToken, nameof(personalAccessToken));

            httpClient.BaseAddress = new Uri("https://api.buddy.works/");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", personalAccessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            httpClient.DefaultRequestHeaders.Add("X-Buddy-Media-Type", "buddy.v1.1.0");

            return new HttpClientFacade(httpClient);
        }
    }
}