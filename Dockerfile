# https://hub.docker.com/_/microsoft-dotnet
# https://github.com/dotnet/dotnet-docker/blob/main/samples/aspnetapp/Dockerfile.alpine-x64
FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /source
# Configure an access token and Credential Manager to connect to the private NuGet feed on Azure DevOps.
# See: https://github.com/dotnet/dotnet-docker/blob/master/documentation/scenarios/nuget-credentials.md
#ARG NUGET_ENDPOINT
#ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS $NUGET_ENDPOINT
#WORKDIR /Users/ContainerUser
#RUN curl --silent https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh | bash

# copy csproj and restore as distinct layers
COPY *.sln .
COPY q5id.platform.email.api/q5id.platform.email.api.csproj ./q5id.platform.email.api/
COPY q5id.platform.email.dal/q5id.platform.email.dal.csproj ./q5id.platform.email.dal/
COPY q5id.platform.email.models/q5id.platform.email.models.csproj ./q5id.platform.email.models/
RUN dotnet restore 

# copy everything else and build app
COPY . .
WORKDIR /source/q5id.platform.email.api
RUN dotnet publish -c release -o /app --self-contained false

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine-amd64
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
EXPOSE 443

# See: https://github.com/dotnet/announcements/issues/20
# Uncomment to enable globalization APIs (or delete)
ENV \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
    LC_ALL=en_US.UTF-8 \
    LANG=en_US.UTF-8
RUN apk add --no-cache icu-libs

ENTRYPOINT ["./q5id.platform.email.api"]
