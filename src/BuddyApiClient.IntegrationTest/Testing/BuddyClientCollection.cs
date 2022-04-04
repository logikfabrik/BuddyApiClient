namespace BuddyApiClient.IntegrationTest.Testing
{
    using Xunit;

    [CollectionDefinition(nameof(BuddyClientCollection))]
    public sealed class BuddyClientCollection : ICollectionFixture<BuddyClientFixture>
    {
    }
}