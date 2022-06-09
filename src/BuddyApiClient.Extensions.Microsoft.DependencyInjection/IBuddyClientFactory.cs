namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    public interface IBuddyClientFactory
    {
        public IBuddyClient Create(string accessToken);
    }
}