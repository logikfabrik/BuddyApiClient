using BuddyApiClient.Extensions.Microsoft.DependencyInjection;
using BuddyApiClient.Extensions.Microsoft.DependencyInjection.Samples.Console;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configuration => { configuration.AddUserSecrets<Program>(); })
    .ConfigureServices((context, services) =>
    {
        services.Configure<BuddyServiceOptions>(context.Configuration);

        services.AddBuddyClient();
        services.AddHostedService<BuddyService>();
    })
    .Build();

await host.RunAsync();