namespace BuddyApiClient
{
    using EnsureThat;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds Buddy API clients to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add clients to.</param>
        /// <param name="namedConfigurationSection"></param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, IConfiguration namedConfigurationSection)
        {
            Ensure.Any.IsNotNull(services, nameof(services));
            Ensure.Any.IsNotNull(namedConfigurationSection, nameof(namedConfigurationSection));

            services.Configure<BuddyClientOptions>(namedConfigurationSection);

            return services.AddClients();
        }

        /// <summary>
        ///     Adds Buddy API clients to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add clients to.</param>
        /// <param name="configureOptions">An action delegate to configure the provided <see cref="BuddyClientOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, Action<BuddyClientOptions> configureOptions)
        {
            Ensure.Any.IsNotNull(services, nameof(services));
            Ensure.Any.IsNotNull(configureOptions, nameof(configureOptions));

            services.Configure(configureOptions);

            return services.AddClients();
        }

        /// <summary>
        ///     Adds Buddy API clients to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add clients to.</param>
        /// <param name="userOptions"></param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, BuddyClientOptions userOptions)
        {
            Ensure.Any.IsNotNull(services, nameof(services));
            Ensure.Any.IsNotNull(userOptions, nameof(userOptions));

            services.AddOptions<BuddyClientOptions>().Configure(options =>
            {
                options.BaseUrl = userOptions.BaseUrl;
                options.AccessToken = userOptions.AccessToken;
            });

            return services.AddClients();
        }

        internal static IServiceCollection AddClients(this IServiceCollection services)
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
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Variables)
                .AddTransient(provider => provider.GetRequiredService<IBuddyClient>().Workspaces);

            return services;
        }
    }
}