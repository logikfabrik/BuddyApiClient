namespace BuddyApiClient.Test
{
    using FluentAssertions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Xunit;

    public sealed class ServiceCollectionExtensionsTest
    {
        public sealed class AddBuddyClient
        {
            [Fact]
            public void Should_Return_Client_When_Added_Using_Configuration_Parameter()
            {
                var configuration = new ConfigurationBuilder()
                    .Build();

                var sut = new ServiceCollection()
                    .AddBuddyClient(configuration)
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Client_When_Added_Using_Action_Parameter()
            {
                var sut = new ServiceCollection()
                    .AddBuddyClient(_ => { })
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }

            [Fact]
            public void Should_Return_Client_When_Added_Using_Options_Parameter()
            {
                var sut = new ServiceCollection()
                    .AddBuddyClient(new BuddyClientOptions())
                    .BuildServiceProvider();

                var buddyClient = sut.GetRequiredService<IBuddyClient>();

                buddyClient.Should().NotBeNull();
            }
        }
    }
}