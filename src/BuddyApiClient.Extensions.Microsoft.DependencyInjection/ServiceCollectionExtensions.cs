namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection
{
    using EnsureThat;
    using global::Microsoft.Extensions.Configuration;
    using global::Microsoft.Extensions.DependencyInjection;
    using global::Microsoft.Extensions.Options;

    public static class ServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds the Buddy API client, and resource specific clients, to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the clients to.</param>
        /// <param name="namedConfigurationSection"></param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, IConfiguration namedConfigurationSection)
        {
            Ensure.Any.IsNotNull(services, nameof(services));
            Ensure.Any.IsNotNull(namedConfigurationSection, nameof(namedConfigurationSection));

            services.Configure<BuddyClientOptions>(namedConfigurationSection);

            services.AddClient();
            services.AddResourceClients();

            return services;
        }

        /// <summary>
        ///     Adds the Buddy API client, and resource specific clients, to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the clients to.</param>
        /// <param name="configureOptions">An action delegate to configure the provided <see cref="BuddyClientOptions" />.</param>
        /// <returns>The <see cref="IServiceCollection" /> so that additional calls can be chained.</returns>
        public static IServiceCollection AddBuddyClient(this IServiceCollection services, Action<BuddyClientOptions> configureOptions)
        {
            Ensure.Any.IsNotNull(services, nameof(services));
            Ensure.Any.IsNotNull(configureOptions, nameof(configureOptions));

            services.Configure(configureOptions);

            services.AddClient();
            services.AddResourceClients();

            return services;
        }

        /// <summary>
        ///     Adds the Buddy API client, and resource specific clients, to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the clients to.</param>
        /// <param name="userOptions">The configured <see cref="BuddyClientOptions" />.</param>
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

            services.AddClient();
            services.AddResourceClients();

            return services;
        }

        /// <summary>
        ///     Adds the Buddy API client to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the client to.</param>
        internal static void AddClient(this IServiceCollection services)
        {
            services.AddHttpClient<IBuddyClient, BuddyClient>((httpClient, provider) =>
            {
                var options = provider.GetRequiredService<IOptions<BuddyClientOptions>>();

                return new BuddyClient(httpClient, options.Value.BaseUrl, options.Value.AccessToken);
            });
        }

        /// <summary>
        ///     Adds resource specific clients to the specified <see cref="IServiceCollection" />.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection" /> to add the clients to.</param>
        internal static void AddResourceClients(this IServiceCollection services)
        {
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
        }
    }
}