# BuddyApiClient.IntegrationTest

## How to run

To run the tests (VS):

1. Run [Buddy On-Premises](https://buddy.works/docs/on-premises) in Docker.

2. Setup Buddy, and get a personal access token:

    1. Browse https://127.0.0.1. The SSL certificate is issued to *buddy.standalone* - this is by design.
    
    2. Setup Buddy; create a user, and workspace.
    
    3. Go to *Workspace Settings* and check *Enable developer API*.
    
    4. Go to *API > Personal Access tokens* and click *Generate a new token*.
    
    5. Enter a name for the token, check all scopes, and click *Add a new API token*.

3. In the Solution Explorer window pane, right-click the *BuddyApiClient.IntegrationTest* project, then click *Manage User Secrets*, and copy-paste:

    ```json
    {
      "BaseUrl": "https://127.0.0.1:443/api/",
      "AccessToken": "YOUR_TOKEN_HERE"
    }
    ```

4. Run the tests.

### How to run Buddy On-Premises in Docker

1. Build the Docker-in-Docker image:

    ```
    docker build -t ubuntu-dind https://github.com/logikfabrik/BuddyApiClient.git#master:docker
    ```

2. Start a container:

    ```
    docker run -itd -p 127.0.0.1:443:443 --privileged --name buddy-on-premises ubuntu-dind
    ```

3. Install Buddy 2.4.60 in the container (in non-interactive mode):

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