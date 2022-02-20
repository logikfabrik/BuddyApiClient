namespace BuddyApiClient;

using System;
using System.Net.Http;
using BuddyApiClient.Core;
using BuddyApiClient.Workspaces;

public sealed class Client : IClient
{
    private readonly Lazy<HttpClientFacade> _httpClientFacade;

    public Client(IHttpClientFactory httpClientFactory, string personalAccessToken)
    {
        _httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClientFactory.CreateClient(), personalAccessToken));

        Workspaces = new WorkspacesClient(_httpClientFacade);
    }

    public Client(HttpClient httpClient, string personalAccessToken)
    {
        _httpClientFacade = new Lazy<HttpClientFacade>(() => HttpClientFacadeFactory.Create(httpClient, personalAccessToken));

        Workspaces = new WorkspacesClient(_httpClientFacade);
    }

    public IWorkspacesClient Workspaces { get; }
}
