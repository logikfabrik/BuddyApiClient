# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works/) API.

## How to get started

Add the BuddyApiClient NuGet to your project:

```
> dotnet add package BuddyApiClient --prerelease
```

Use a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction), to access the Buddy API.

On app start-up, add BuddyApiClient to your service collection:

```csharp
services.AddBuddyClient(new BuddyClientOptions { AccessToken = "" });
```

Take a dependence on `IBuddyClient` and qyery the Buddy API.

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.