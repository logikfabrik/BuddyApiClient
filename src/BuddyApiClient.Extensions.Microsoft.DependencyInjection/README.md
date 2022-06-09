# BuddyApiClient.Extensions.Microsoft.DependencyInjection

BuddyApiClient extensions for Microsoft.Extensions.DependencyInjection. BuddyApiClient is a .NET client for the Buddy (https://buddy.works) API.

## How to use

1. Add the [BuddyApiClient.Extensions.Microsoft.DependencyInjection NuGet](https://www.nuget.org/packages/BuddyApiClient.Extensions.Microsoft.DependencyInjection) to your project:

    ```
    dotnet add package BuddyApiClient.Extensions.Microsoft.DependencyInjection --prerelease
    ```

2. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction), to access the Buddy API.

3. On app start-up, use any of the `AddBuddyClient(...)` extension method overloads to register the Buddy clients. E.g:

    ```csharp
    services.AddBuddyClient(new BuddyClientOptions { AccessToken = "YOUR_TOKEN_HERE" });
    ```

4. Next, take a dependence on `IBuddyClient` (or any resource specific client interface, e.g. `IMembersClient`, `IProjectsClient`, and `IVariablesClient`), and query the Buddy API.

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.