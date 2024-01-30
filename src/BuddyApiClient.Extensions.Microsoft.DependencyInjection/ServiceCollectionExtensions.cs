namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    using global::Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds BuddyApiClient services to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the services to.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services)
        {
            services.AddHttpClient<IBuddyClientFactory, BuddyClientFactory>();

            services.AddTransient<CreateBuddyClient>(provider =>
            {
                using var scope = provider.CreateScope();

                return scope.ServiceProvider.GetRequiredService<IBuddyClientFactory>().Create;
            });

            return services;
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, Action<BuddyClientOptions> configureOptions)
        {
            services
                .ConfigureOptions(configureOptions);

            AddBuddyClient(services);

            return services;
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, BuddyClientOptions userOptions)
        {
            services
                .AddOptions<BuddyClientOptions>()
                .Configure(options =>
                {
                    options.BaseUrl = userOptions.BaseUrl;
                });

            AddBuddyClient(services);

            return services;
        }
    }
}