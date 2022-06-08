namespace BuddyApiClient.IntegrationTest.Testing
{
    [CollectionDefinition(nameof(BuddyClientCollection))]
    public sealed class BuddyClientCollection : ICollectionFixture<BuddyClientFixture>
    {
    }
}