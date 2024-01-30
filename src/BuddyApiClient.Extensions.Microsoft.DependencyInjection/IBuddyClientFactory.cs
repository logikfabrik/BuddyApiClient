namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    public interface IBuddyClientFactory
    {
        IBuddyClient Create(string accessToken);
    }
}