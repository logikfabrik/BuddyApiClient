# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works/) API.

## How to get started

Add the BuddyApiClient NuGet to your project:

```
PM> Install-Package BuddyApiClient
```

Use a OAuth2 access token, a [Personal Access Token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or [Basic Authorization](https://buddy.works/docs/api/getting-started/oauth2/introduction#basic-authorization), to access the Buddy API.

On app start-up, add BuddyApiClient to your service collection:

```csharp
services.AddBuddyClient(new BuddyClientOptions { AccessToken = "" });
```

Or:

```csharp
services.AddBuddyClient(new BuddyClientOptions { BasicAuthClientId = "", BasicAuthClientSecret = "" });
```

Query the Buddy API using `BuddyApiClient.IClient`.

## How to contribute

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.