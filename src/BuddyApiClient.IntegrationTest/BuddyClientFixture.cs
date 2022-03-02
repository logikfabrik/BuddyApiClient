namespace BuddyApiClient.IntegrationTest
{
    using System.Net.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;

    public sealed class BuddyClientFixture
    {
        public BuddyClientFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<BuddyClientFixture>()
                .Build();

            BuddyClient = new BuddyClient(new HttpClient(), new OptionsWrapper<BuddyClientOptions>(configuration.Get<BuddyClientOptions>()));
        }

        public IBuddyClient BuddyClient { get; }
    }
}