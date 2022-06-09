# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works) API.

## How to use

1. Add the [BuddyApiClient NuGet](https://www.nuget.org/packages/BuddyApiClient) to your project:

    ```
    dotnet add package BuddyApiClient --prerelease
    ```

2. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction), to access the Buddy API.

3. Create an instance of `BuddyClient`, and query the Buddy API. E.g:

    ```csharp
    var client = new BuddyClient(new Uri("https://api.buddy.works"), "YOUR_TOKEN_HERE");

    var projects = await client.Projects.List(new Domain("YOUR_DOMAIN_HERE"));
    ```

If you're using Microsoft.Extensions.DependencyInjection, see [BuddyApiClient.Extensions.Microsoft.DependencyInjection](https://github.com/logikfabrik/BuddyApiClient/blob/master/src/BuddyApiClient.Extensions.Microsoft.DependencyInjection).

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.