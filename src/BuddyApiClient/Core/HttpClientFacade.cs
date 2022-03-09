namespace BuddyApiClient.Core
{
    using System.Net;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using BuddyApiClient.Core.Models.Response;
    using EnsureThat;

    internal sealed class HttpClientFacade
    {
        private readonly HttpClient _httpClient;

        public HttpClientFacade(HttpClient httpClient)
        {
            _httpClient = Ensure.Any.HasValue(httpClient, nameof(httpClient));
        }

        public async Task<T?> Get<T>(string url, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(url, nameof(url));

            using var response = await _httpClient.GetAsync(url, cancellationToken);

            await ThrowOnError(response, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
        }

        public async Task<T?> Post<T>(string url, object content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(url, nameof(url));
            Ensure.Any.HasValue(content, nameof(content));

            using var response = await _httpClient.PostAsync(url, JsonContent.Create(content), cancellationToken);

            await ThrowOnError(response, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
        }

        public async Task<T?> Patch<T>(string url, object content, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(url, nameof(url));
            Ensure.Any.HasValue(content, nameof(content));

            using var response = await _httpClient.PatchAsync(url, JsonContent.Create(content), cancellationToken);

            await ThrowOnError(response, cancellationToken);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken);
        }

        public async Task Delete(string url, CancellationToken cancellationToken = default)
        {
            Ensure.String.IsNotNullOrWhiteSpace(url, nameof(url));

            using var response = await _httpClient.DeleteAsync(url, cancellationToken);

            await ThrowOnError(response, cancellationToken);

            response.EnsureSuccessStatusCode();
        }

        private static async Task ThrowOnError(HttpResponseMessage response, CancellationToken cancellationToken)
        {
            if (response.StatusCode is not (HttpStatusCode.BadRequest or HttpStatusCode.Forbidden))
            {
                return;
            }

            await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);

            if (stream.Length == 0)
            {
                return;
            }

            var errors = (await JsonSerializer.DeserializeAsync<ErrorResponse>(stream, cancellationToken: cancellationToken))?.Errors?.ToArray() ?? Array.Empty<Error>();

            if (!errors.Any())
            {
                return;
            }

            var messageBuilder = new StringBuilder();

            foreach (var error in errors)
            {
                messageBuilder.AppendLine(error.Message);
            }

            var message = messageBuilder.ToString().Trim();

            throw new HttpRequestException(message, null, response.StatusCode);
        }
    }
}