namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Test
{
    using global::Microsoft.Extensions.DependencyInjection;

    public sealed class ServiceCollectionExtensionsTest
    {
        private static bool IsDescriptor<T>(ServiceDescriptor descriptor)
        {
            return typeof(T).IsAssignableFrom(descriptor.ServiceType);
        }

        public sealed class AddBuddyClient
        {
            [Fact]
            public void Should_AddACreateBuddyClientServiceDescriptor()
            {
                var sut = new ServiceCollection().AddBuddyClient();

                sut.Any(IsDescriptor<CreateBuddyClient>).Should().BeTrue();
            }
        }
    }
}