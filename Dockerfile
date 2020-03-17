FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

#Set workdir
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY NuGet.config .
COPY LosExpress/*.csproj ./

ARG BMWAF_NUGET_USER
ARG BMWAF_NUGET_PASS
ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0
ENV BMWAF_NUGET_USER=${BMWAF_NUGET_USER}
ENV BMWAF_NUGET_PASS=${BMWAF_NUGET_PASS}

RUN dotnet restore --configfile ./NuGet.config 

COPY . ./
RUN dotnet publish LosExpress/*.csproj -c Release -o out

#Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

ARG GIT_LAST_COMMIT_SHA
ARG CLUSTER_SERVICE_CURRENT_VERSION
ENV Internal__Info__CommitSha=${GIT_LAST_COMMIT_SHA}
ENV Internal__Info__ClusterServiceVersion=${CLUSTER_SERVICE_CURRENT_VERSION}

COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "LosExpress.dll"]