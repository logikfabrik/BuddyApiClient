namespace BuddyApiClient.IntegrationTest.Testing
{
    using System.Threading.Tasks;
    using Xunit;

    [Collection(nameof(BuddyClientCollection))]
    public abstract class BuddyClientTest : IAsyncLifetime
    {
        protected BuddyClientTest(BuddyClientFixture fixture)
        {
            Fixture = fixture;
        }

        protected BuddyClientFixture Fixture { get; }

        public virtual async Task InitializeAsync()
        {
            await Task.CompletedTask;
        }

        public virtual async Task DisposeAsync()
        {
            await Task.CompletedTask;
        }
    }
}