namespace BuddyApiClient
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, IConfiguration namedConfigurationSection)
        {
            services.Configure<BuddyClientOptions>(namedConfigurationSection);

            services.AddHttpClient<IBuddyClient, BuddyClient>();

            return services;
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, Action<BuddyClientOptions> configureOptions)
        {
            services.Configure(configureOptions);

            services.AddHttpClient<IBuddyClient, BuddyClient>();

            return services;
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, BuddyClientOptions userOptions)
        {
            services.AddOptions<BuddyClientOptions>().Configure(options =>
            {
                options.BaseUrl = userOptions.BaseUrl;
                options.AccessToken = userOptions.AccessToken;
            });

            services.AddHttpClient<IBuddyClient, BuddyClient>();

            return services;
        }
    }
}