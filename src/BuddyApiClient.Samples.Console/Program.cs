using BuddyApiClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var client = new BuddyClient(configuration["AccessToken"]);

var workspaces = await client.Workspaces.List();

if (workspaces is not null)
{
    foreach (var workspace in workspaces.Workspaces)
    {
        Console.WriteLine(workspace.Name);
    }
}