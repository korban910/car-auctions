# GitHub Action

[← Back to Home](./README.md)

### Actions

In the `root` folder create `.github` folder. Then inside `.github` folder, create `workflows` folder. Inside the `workflows` folder create `deploy.yml` file. 

deploy.yml:
```
name: Build and publish

on:
  workflow_dispatch: 
  push:
    branches: [ "main" ]

jobs:
  build-and-push:
    runs-on: ubuntu-latest
    env:
      continue: 'false'
    strategy:
      matrix: 
        service:
          - name: 'korban910/auction-svc'
            path: 'src/AuctionService'
          - name: 'korban910/bid-svc'
            path: 'src/BiddingService'
          - name: 'korban910/search-svc'
            path: 'src/SearchService'
          - name: 'korban910/notify-svc'
            path: 'src/NotificationService'
          - name: 'korban910/identity-svc'
            path: 'src/IdentityService'
          - name: 'korban910/gateway-svc'
            path: 'src/GatewayService'
          - name: 'korban910/web-app'
            path: 'frontend/web-app'
    steps:
      - name: Checkout code
        uses: actions/checkout@v4
        with:
          fetch-depth: 2
      
      - name: Check for changes in serivce path
        run: |
          if git diff --quite HEAD^ HEAD -- ${{matrix.service.path}}; then
            echo "No changes in ${{matrix.service.path}}. Skipping build"
            echo "continue=false" >> $GITHUB_ENV
          else
            echo "Changes detected in ${{matrix.service.path}}. Proceeding with build"
            echo "continue=true" >> $GITHUB_ENV
          fi
        
      - name: Set up Docker buildx
        if: env.continue == 'true'
        uses: docker/setup-buildx-action@v2
      
      - name: Login to Docker
        if: env.continue == 'true'
        uses: docker/login-action@v3
        with:
          username: ${{secrets.DOCKER_USERNAME}}
          password: ${{secrets.DOCKER_TOKEN}}
      
      - name: Build and push docker image
        if: env.continue == 'true'
        uses: docker/build-push-action@v6
        with:
          context: .
          file: ${{matrix.service.path}}/Dockerfile
          push: true
          tags: ${{matrix.service.name}}:latest

```

### GitHub

![Settings](./images/GitHub_Settings.png)

![Actions](./images/GitHub_Actions.png)

![Username](./images/GitHub_Username.png)

![Env](./images/GitHub_Env.png)

![Token](./images/DockerHub_Token.png)

![Generate](./images/Generate_Token.png)

From above generate the `Token` then assign it same as `Username` in the `GitHub`.

![Set up](./images/GitHub_Env_Done.png)


### Run manually

```
gh workflow run deploy.yml --ref <Branch_name>
```