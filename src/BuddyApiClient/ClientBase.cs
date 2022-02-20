namespace BuddyApiClient;

using System;
using BuddyApiClient.Core;
using EnsureThat;

internal abstract class ClientBase
{
    private readonly Lazy<HttpClientFacade> _httpClientFacade;

    protected ClientBase(Lazy<HttpClientFacade> httpClientFacade) => _httpClientFacade = Ensure.Any.HasValue(httpClientFacade, nameof(httpClientFacade));

    protected HttpClientFacade HttpClientFacade => _httpClientFacade.Value;
}
