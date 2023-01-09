namespace BuddyApiClient
{
    using BuddyApiClient.Core;

    internal abstract class ClientBase
    {
        private readonly Lazy<HttpClientFacade> _httpClientFacade;

        protected ClientBase(Lazy<HttpClientFacade> httpClientFacade)
        {
            _httpClientFacade = httpClientFacade;
        }

        protected HttpClientFacade HttpClientFacade => _httpClientFacade.Value;
    }
}