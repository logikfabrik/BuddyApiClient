namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Test
{
    using global::Microsoft.Extensions.DependencyInjection;
    using global::Microsoft.Extensions.Options;

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

            [Fact]
            public void Should_AddACreateBuddyClientServiceDescriptor_When_CalledWithConfigure()
            {
                var configure = new Action<BuddyClientOptions>(options => { });

                var sut = new ServiceCollection().AddBuddyClient(configure);

                sut.Any(IsDescriptor<CreateBuddyClient>).Should().BeTrue();
            }

            [Fact]
            public void Should_AddAIConfigureOptionsServiceDescriptor_When_CalledWithConfigure()
            {
                var configure = new Action<BuddyClientOptions>(options => { });

                var sut = new ServiceCollection().AddBuddyClient(configure);

                sut.Any(IsDescriptor<IConfigureOptions<BuddyClientOptions>>).Should().BeTrue();
            }
        }
    }
}