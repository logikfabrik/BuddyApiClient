# BuddyApiClient

A .NET client for the [Buddy](https://buddy.works) API.

## How to use BuddyApiClient

1. Add the BuddyApiClient NuGet to your project:

    ```
    dotnet add package BuddyApiClient --prerelease
    ```

2. Get a [personal access token](https://buddy.works/docs/api/getting-started/oauth2/personal-access-token), or a [OAuth2 access token](https://buddy.works/docs/api/getting-started/oauth2/introduction), to access the Buddy API.

3. On app start-up, add BuddyApiClient to your service collection:

    ```csharp
    services.AddBuddyClient(new BuddyClientOptions { AccessToken = "" });
    ```

4. Next, take a dependence on `IBuddyClient`, and query the Buddy API.

## How to contribute to BuddyApiClient

BuddyApiClient is Open Source (MIT), and you're welcome to contribute!

If you have a bug report, feature request, or suggestion, please open a new issue. To submit a patch, please send a pull request.

### How to run the integration tests

To run the integration tests, run [Buddy On-Premises](https://buddy.works/docs/on-premises) in Docker. Set up a Buddy user, workspace, and personal access token. Then configure BuddyApiClient.IntegrationTest in VS, and run the tests.

#### How to create the Buddy container

1. Get [Docker](https://docs.docker.com/get-docker).

2. Build the Docker-in-Docker image:

    ```
    docker build -t ubuntu-dind https://github.com/logikfabrik/BuddyApiClient.git#master:docker
    ```

3. Start a container:

    ```
    docker run -itd -p 127.0.0.1:443:443 --privileged --name buddy-on-premises ubuntu-dind
    ```

4. Install Buddy in the container (in non-interactive mode):

    ```
    docker exec -it buddy-on-premises /bin/bash -c "curl -sSL https://get.buddy.works | sh && sudo buddy --yes --ver 2.4.60 install"
    ```

5. Done!

#### How to start the Buddy container, if stopped

1. Start the container:

    ```
    docker start buddy-on-premises
    ```

2. Start Buddy:

    ```
    docker exec -it buddy-on-premises /bin/bash -c "buddy start"
    ```

3. Done!

#### How to set up Buddy (and to generate a new token)

1. Start the Buddy container, if stopped.

2. Browse https://127.0.0.1. The SSL certificate will be issued to *buddy.standalone*.

3. Set up a Buddy user, and workspace.

4. Go to *Workspace Settings* and check *Enable developer API*.

5. Go to *API > Personal Access tokens* and click *Generate a new token*.

6. Enter a name for the token, check all scopes, and click *Add a new API token*.

7. Copy the token.

8. Done!

#### How to configure BuddyApiClient.IntegrationTest in VS

1. Open *BuddyApiClient.sln* in VS.

2. In the Solution Explorer window pane, right-click *BuddyApiClient.IntegrationTest*, then click *Manage User Secrets*.

3. ...