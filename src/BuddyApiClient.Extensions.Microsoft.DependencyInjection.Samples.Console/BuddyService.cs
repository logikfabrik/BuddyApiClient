namespace BuddyApiClient.Extensions.Microsoft.DependencyInjection.Samples.Console
{
    using global::Microsoft.Extensions.Hosting;
    using global::Microsoft.Extensions.Options;

    public sealed class BuddyService : IHostedService
    {
        private readonly IBuddyClient _client;

        public BuddyService(IOptions<BuddyServiceOptions> options, CreateBuddyClient createBuddyClient)
        {
            _client = createBuddyClient(options.Value.AccessToken);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var workspaces = await _client.Workspaces.List(cancellationToken);

            if (workspaces is not null)
            {
                foreach (var workspace in workspaces.Workspaces)
                {
                    System.Console.WriteLine(workspace.Name);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}