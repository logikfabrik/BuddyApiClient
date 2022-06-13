# BuddyApiClient.Extensions.Microsoft.DependencyInjection

BuddyApiClient extensions for Microsoft.Extensions.DependencyInjection. BuddyApiClient is a .NET client for the [Buddy](https://buddy.works) API.

## How to use

1. Add the [BuddyApiClient.Extensions.Microsoft.DependencyInjection NuGet](https://www.nuget.org/packages/BuddyApiClient.Extensions.Microsoft.DependencyInjection) to your project:

    ```
    dotnet add package BuddyApiClient.Extensions.Microsoft.DependencyInjection --prerelease
    ```

2. On app start-up, add BuddyApiClient services to the container:

    ```csharp
    builder.Services.AddBuddyClient();
    ```

3. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction).

4. Next, take a dependence on the `CreateBuddyClient` delegate, create a client, and query the Buddy API:

    ```csharp
    var client = createBuddyClient("YOUR_TOKEN_HERE");

    var workspaces = await client.Workspaces.List();
    ```

See the [console app sample](https://github.com/logikfabrik/BuddyApiClient/tree/master/src/BuddyApiClient.Extensions.Microsoft.DependencyInjection.Samples.Console).

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.