using Microsoft.Extensions.Options;

namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Test
{
    public sealed class BuddyClientFactoryTest
    {
        public sealed class Create
        {
            [Fact]
            public void Should_ReturnAnIBuddyClientInstance()
            {
                var sut = new BuddyClientFactory(new HttpClient(), Options.Create(new BuddyClientOptions()));

                var client = sut.Create(string.Empty);

                client.Should().BeAssignableTo<IBuddyClient>();
            }
        }
    }
}