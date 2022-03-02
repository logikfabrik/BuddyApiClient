﻿namespace BuddyApiClient.Core
{
    using System.Net.Http.Headers;
    using System.Net.Mime;
    using EnsureThat;

    internal static class HttpClientFacadeFactory
    {
        public static HttpClientFacade Create(HttpClient httpClient, Uri? baseUrl, string? accessToken)
        {
            Ensure.Any.HasValue(httpClient, nameof(httpClient));

            httpClient.BaseAddress = baseUrl;

            if (accessToken is not null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("BuddyApiClient", null));
            httpClient.DefaultRequestHeaders.Add("X-Buddy-Media-Type", "buddy.v1.1.0");

            return new HttpClientFacade(httpClient);
        }
    }
}