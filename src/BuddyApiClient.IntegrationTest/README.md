# BuddyApiClient.IntegrationTest

## How to run the integration tests

To run the integration tests, run [Buddy On-Premises](https://buddy.works/docs/on-premises) in Docker. Set up a Buddy user, and workspace, and generate a personal access token. Then add config to the BuddyApiClient.IntegrationTest project in VS, and run the tests.

### How to create the Buddy container

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

#### How to start the Buddy container, if stopped

1. Start the container:

    ```
    docker start buddy-on-premises
    ```

2. Start Buddy:

    ```
    docker exec -it buddy-on-premises /bin/bash -c "buddy start"
    ```

### How to set up Buddy (and generate a personal access token)

1. Start the Buddy container, if stopped.

2. Browse https://127.0.0.1. The SSL certificate will be issued to *buddy.standalone* - this is by design.

3. Set up a Buddy user, and workspace.

4. Go to *Workspace Settings* and check *Enable developer API*.

5. Go to *API > Personal Access tokens* and click *Generate a new token*.

6. Enter a name for the token, check all scopes, and click *Add a new API token*.

7. Copy the token, and use it in place of YOUR_TOKEN_HERE in this README.

### How to add config to the BuddyApiClient.IntegrationTest project in VS

1. Open the solution in VS.

2. In the Solution Explorer window pane, right-click the *BuddyApiClient.IntegrationTest* project, then click *Manage User Secrets*.

3. Add config to `secrets.json`:

    ```json
    {
      "BaseUrl": "https://127.0.0.1:443/api/",
      "AccessToken": "YOUR_TOKEN_HERE"
    }
    ```