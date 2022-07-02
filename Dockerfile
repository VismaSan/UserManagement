FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
ARG PROJECT=UserManagement

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY ${PROJECT}.Api/*.csproj ./${PROJECT}.Api/
COPY ${PROJECT}.Services/*.csproj ./${PROJECT}.Services/
COPY ${PROJECT}.Repositories/*.csproj ./${PROJECT}.Repositories/
RUN dotnet restore

# Copy everything else and build
COPY ${PROJECT}.Api/. ./${PROJECT}.Api/
COPY ${PROJECT}.Services/. ./${PROJECT}.Services/
COPY ${PROJECT}.Repositories/. ./${PROJECT}.Repositories/
WORKDIR /src/${PROJECT}.Api
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT ["dotnet", "UserManagement.Api.dll"]