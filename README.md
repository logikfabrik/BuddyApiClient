# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works/) API.

## How To

### Basic usage

1)

Add a reference to BuddyApiClient to your project.

2)

On app start-up, add BuddyApiClient to your service collection.

Add with use of `IHttpClientFactory`:

```csharp
services.AddHttpClient();
services.AddTransient<BuddyApiClient.IClient, BuddyApiClient.Client>();
```

Or with use of `HttpClient`:

```csharp
services.AddHttpClient<BuddyApiClient.IClient, BuddyApiClient.Client>();
```

For more information, see:
- [Dependency injection in .NET](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Use IHttpClientFactory to implement resilient HTTP requests](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests)

3)

Query the Buddy API using `BuddyApiClient.IClient`.

## Contributions
BuddyApiClient is Open Source (MIT), and you’re welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.