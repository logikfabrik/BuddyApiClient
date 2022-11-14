using BuddyApiClient;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var accessToken = configuration["AccessToken"];

var client = new BuddyClient(accessToken);

var workspaces = await client.Workspaces.List();

if (workspaces is not null)
{
    foreach (var workspace in workspaces.Workspaces)
    {
        Console.WriteLine(workspace.Name);
    }
}