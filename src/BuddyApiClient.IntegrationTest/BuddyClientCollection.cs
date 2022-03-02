namespace BuddyApiClient.IntegrationTest
{
    using Xunit;

    [CollectionDefinition(nameof(BuddyClientCollection))]
    public sealed class BuddyClientCollection : ICollectionFixture<BuddyClientFixture>
    {
    }
}