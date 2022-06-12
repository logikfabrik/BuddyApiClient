namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    using EnsureThat;
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
            Ensure.Any.HasValue(services, nameof(services));

            services.AddHttpClient<IBuddyClientFactory, BuddyClientFactory>();

            services.AddTransient<CreateBuddyClient>(provider =>
            {
                using var scope = provider.CreateScope();

                return scope.ServiceProvider.GetRequiredService<IBuddyClientFactory>().Create;
            });

            return services;
        }
    }
}