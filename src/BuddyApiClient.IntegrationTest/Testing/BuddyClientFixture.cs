namespace BuddyApiClient.IntegrationTest.Testing
{
    using System.Net.Security;
    using System.Security.Cryptography.X509Certificates;
    using Microsoft.Extensions.Configuration;

    public sealed class BuddyClientFixture
    {
        public BuddyClientFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddUserSecrets<BuddyClientFixture>()
                .Build();

            var accessToken = configuration["AccessToken"];

            var baseUrl = configuration["BaseUrl"];

            var httpClientHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = SslCertificateValidator };
            
            var httpClient = new HttpClient(httpClientHandler);

            BuddyClient = new BuddyClient(accessToken, httpClient, new Uri(baseUrl));
        }

        public IBuddyClient BuddyClient { get; }

        private static bool SslCertificateValidator(HttpRequestMessage? requestMessage, X509Certificate2? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}