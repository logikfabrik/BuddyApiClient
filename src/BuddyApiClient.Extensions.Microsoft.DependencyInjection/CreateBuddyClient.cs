namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    public delegate IBuddyClient CreateBuddyClient(string accessToken, Uri? baseUrl = null);
}