# BuddyApiClient.Extensions.Microsoft.DependencyInjection

[BuddyApiClient](https://github.com/logikfabrik/BuddyApiClient) extensions for Microsoft.Extensions.DependencyInjection. BuddyApiClient is a .NET client for the [Buddy](https://buddy.works) API.

## How to use

1. Add the [BuddyApiClient.Extensions.Microsoft.DependencyInjection](https://www.nuget.org/packages/BuddyApiClient.Extensions.Microsoft.DependencyInjection) NuGet to your project:

    ```
    dotnet add package BuddyApiClient.Extensions.Microsoft.DependencyInjection --prerelease
    ```

2. On app start-up, add BuddyApiClient to your container:

    ```csharp
    builder.Services.AddBuddyClient();
    ```

3. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction).

4. Use the `CreateBuddyClient` factory delegate to create an instance of `IBuddyClient`, and query the Buddy API using your token. E.g:

    ```csharp
    public class BuddyService
    {
        private readonly IBuddyClient _client;

        public BuddyService(CreateBuddyClient createBuddyClient)
        {
            _client = createBuddyClient("YOUR_TOKEN_HERE");
        }
    }
    ```

See the [console app sample](https://github.com/logikfabrik/BuddyApiClient/tree/master/src/BuddyApiClient.Extensions.Microsoft.DependencyInjection.Samples.Console).

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.