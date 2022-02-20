namespace BuddyApiClient.Core;

using System.Net.Http.Json;
using EnsureThat;

internal sealed class HttpClientFacade
{
    private readonly HttpClient _httpClient;

    public HttpClientFacade(HttpClient httpClient)
    {
        _httpClient = Ensure.Any.HasValue(httpClient, nameof(httpClient));
    }

    public async Task<T?> Get<T>(string url, CancellationToken cancellationToken)
    {
        Ensure.String.IsNotNullOrWhiteSpace(url, nameof(url));

        using var response = await _httpClient.GetAsync(url, cancellationToken);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
    }
}
