namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Test
{
    public sealed class ServiceProviderTest
    {
        public sealed class GetService
        {
            [Fact]
            public void Should_ReturnACreateBuddyClientDelegate()
            {
                var sut = new ServiceCollection().AddBuddyClient().BuildServiceProvider();

                var factory = sut.GetService<CreateBuddyClient>();

                factory.Should().NotBeNull();
            }
        }
    }
}