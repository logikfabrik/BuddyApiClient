# Sample: BuddyApiClient

A sample console app, getting workspaces from the [Buddy](https://buddy.works) API.

## How to run

To run the sample (VS):

1. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token).

2. In the Solution Explorer window pane, right-click the *BuddyApiClient.Samples.Console* project, then click *Manage User Secrets*, and copy-paste:

    ```json
    {
      "AccessToken": "YOUR_TOKEN_HERE"
    }
    ```

4. Set *BuddyApiClient.Samples.Console* as the startup project, and start the app.