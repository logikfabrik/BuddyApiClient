﻿namespace BuddyApiClient.Core
{
    using System.Net;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using BuddyApiClient.Core.Models.Response;

    internal sealed class HttpClientFacade
    {
        private readonly string _accessToken;
        private readonly HttpClient _httpClient;
        private readonly Uri _baseUrl;

        public HttpClientFacade(string accessToken, HttpClient httpClient, Uri baseUrl)
        {
            ArgumentNullException.ThrowIfNull(accessToken);
            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(baseUrl);

            _accessToken = accessToken;
            _httpClient = httpClient;
            _baseUrl = baseUrl;
        }

        public async Task<T?> Get<T>(string url, JsonSerializerOptions? deserializationOptions = null, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(url);

            using var request = CreateRequest(HttpMethod.Get, url);

            using var response = await SendRequest(request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<T>(deserializationOptions, cancellationToken);
        }

        public async Task<T?> Post<T>(string url, object content, JsonSerializerOptions? deserializationOptions = null, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(content);

            using var request = CreateRequest(HttpMethod.Post, url, JsonContent.Create(content));

            using var response = await SendRequest(request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<T>(deserializationOptions, cancellationToken);
        }

        public async Task<T?> Patch<T>(string url, object content, JsonSerializerOptions? deserializationOptions = null, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(url);
            ArgumentNullException.ThrowIfNull(content);

            using var request = CreateRequest(HttpMethod.Patch, url, JsonContent.Create(content, options: new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault }));

            using var response = await SendRequest(request, cancellationToken);

            return await response.Content.ReadFromJsonAsync<T>(deserializationOptions, cancellationToken);
        }

        public async Task Delete(string url, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(url);

            using var request = CreateRequest(HttpMethod.Delete, url);

            using var response = await SendRequest(request, cancellationToken);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, HttpContent? content = null)
        {
            var request = new HttpRequestMessage(method, new Uri(_baseUrl, url));

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

            if (content is not null)
            {
                request.Content = content;
            }

            return request;
        }

        private async Task<HttpResponseMessage> SendRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await _httpClient.SendAsync(request, cancellationToken);

            await ThrowOnError(response, cancellationToken);

            response.EnsureSuccessStatusCode();

            return response;
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

            var errors = (await JsonSerializer.DeserializeAsync<ErrorResponse>(stream, cancellationToken: cancellationToken))?.Errors.ToArray() ?? Array.Empty<Error>();

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