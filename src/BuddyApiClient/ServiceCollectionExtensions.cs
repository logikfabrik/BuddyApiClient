namespace BuddyApiClient
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, IConfiguration namedConfigurationSection)
        {
            services.Configure<BuddyClientOptions>(namedConfigurationSection);

            return services.AddBuddyClients();
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, Action<BuddyClientOptions> configureOptions)
        {
            services.Configure(configureOptions);

            return services.AddBuddyClients();
        }

        public static IServiceCollection AddBuddyClient(this IServiceCollection services, BuddyClientOptions userOptions)
        {
            services.AddOptions<BuddyClientOptions>().Configure(options =>
            {
                options.BaseUrl = userOptions.BaseUrl;
                options.AccessToken = userOptions.AccessToken;
            });

            return services.AddBuddyClients();
        }

        internal static IServiceCollection AddBuddyClients(this IServiceCollection services)
        {
            services.AddHttpClient<IBuddyClient, BuddyClient>();

            services
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().CurrentUser)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().CurrentUserEmails)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().GroupMembers)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Groups)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Members)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().PermissionSets)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().ProjectGroups)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().ProjectMembers)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Projects)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Workspaces);

            return services;
        }
    }
}