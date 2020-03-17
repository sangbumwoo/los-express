# los-express

## Getting started

### Setting up Docker locally:

- https://pages.code.connected.bmw/runtime/docs/developer-guides/docker/local-docker-setup/

### Authenticating with vault:

- https://suus0001.w10:8090/display/ARC/Using+Vault

### MacOS Development IDEs:

(In order of "proven-ness")
- https://www.jetbrains.com/rider/download/
- https://code.visualstudio.com/
- https://visualstudio.microsoft.com/vs/mac/

### Create your Github Repo:
https://code.connected.bmw/runtime/github-config

### Create your Jenkins pipeline:
http://suus0003.w10:7990/projects/BTCRUN/repos/jenkins-jobs/browse

### (Optional) Provision LaunchDarkly flags:
http://suus0003.w10:7990/projects/BTCRUN/repos/launch-darkly-config/browse

## Running locally
You may run AP.NET Core apps directly on your development machine using Kestrel, or you may start them inside of a Docker container. Either of the approaches listed below will start up the app locally listening on http://localhost:5000


### Run using Kestrel:
```sh
dotnet run --project LosExpress
```

### Run using Docker:
```sh
docker build --build-arg BMWAF_NUGET_USER=$BMWAF_NUGET_USER --build-arg BMWAF_NUGET_PASS=$BMWAF_NUGET_PASS -t aspnetapp .
docker docker run -d -p 5000:80 --name myapp aspnetapp
```

## Running unit tests locally

### Run unit tests with line coverage
`dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:Threshold=100 /p:ThresholdType=line` 

##### References

Unit Testing with MSTest: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-mstest

Mock Dependencies with Moq: https://github.com/moq/moq4

Code Coverage using Coverlet: https://github.com/tonerdo/coverlet

Code Coverage report generator using Report Generator: https://github.com/danielpalme/ReportGenerator/tree/v4#usage