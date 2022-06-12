namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Test
{
    public sealed class BuddyClientFactoryTest
    {
        public sealed class Create
        {
            [Fact]
            public void Should_ReturnAnIBuddyClientInstance()
            {
                var sut = new BuddyClientFactory(new HttpClient());

                var client = sut.Create(string.Empty, new Uri("https://api.buddy.works"));

                client.Should().BeAssignableTo<IBuddyClient>();
            }
        }
    }
}