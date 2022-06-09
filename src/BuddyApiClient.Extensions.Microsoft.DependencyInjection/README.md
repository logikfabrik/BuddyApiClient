# BuddyApiClient.Extensions.Microsoft.DependencyInjection

BuddyApiClient extensions for Microsoft.Extensions.DependencyInjection. BuddyApiClient is a .NET client for the Buddy (https://buddy.works) API.

## How to use

1. Add the [BuddyApiClient.Extensions.Microsoft.DependencyInjection NuGet](https://www.nuget.org/packages/BuddyApiClient.Extensions.Microsoft.DependencyInjection) to your project:

    ```
    dotnet add package BuddyApiClient.Extensions.Microsoft.DependencyInjection --prerelease
    ```

2. On app start-up, add BuddyApiClient services to the container:

    ```csharp
    builder.Services.AddBuddyClient();
    ```

3. Next, take a dependence on the `CreateBuddyClient` delegate, create a client, and query the Buddy API:

    ```csharp
    var client = createBuddyClient("YOUR_TOKEN_HERE");

    var projects = await client.Projects.List(new Domain("YOUR_DOMAIN_HERE"));
    ```

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.