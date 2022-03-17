namespace BuddyApiClient.IntegrationTest
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public sealed class BuddyClientFixture
    {
        public BuddyClientFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<BuddyClientFixture>()
                .Build();

            var serviceProvider = new ServiceCollection()
                .AddBuddyClient(configuration)
                .BuildServiceProvider();

            BuddyClient = serviceProvider.GetRequiredService<IBuddyClient>();
        }

        public IBuddyClient BuddyClient { get; }
    }
}