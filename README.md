# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works/) API.

## How To
Add a reference to BuddyApiClient to your project.

### Basic usage

```csharp
services.AddHttpClient();
services.AddTransient<BuddyApiClient.IClient, BuddyApiClient.Client>();
```

```csharp
services.AddHttpClient<BuddyApiClient.IClient, BuddyApiClient.Client>();
```

## Contributions
BuddyApiClient is Open Source (MIT), and you’re welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.