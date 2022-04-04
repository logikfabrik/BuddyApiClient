namespace BuddyApiClient.IntegrationTest.Testing
{
    using System.Net.Http;
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
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
                .AddHttpClient(nameof(IBuddyClient))
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = SslCertificateValidator })
                .Services
                .BuildServiceProvider();

            BuddyClient = serviceProvider.GetRequiredService<IBuddyClient>();
        }

        public IBuddyClient BuddyClient { get; }

        private static bool SslCertificateValidator(HttpRequestMessage? requestMessage, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}