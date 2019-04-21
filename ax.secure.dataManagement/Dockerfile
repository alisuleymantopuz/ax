FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5100

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ax.secure.dataManagement/ax.secure.dataManagement.csproj ax.secure.dataManagement/
COPY ax.secure.dataManagement.integration.Tests/ax.secure.dataManagement.integration.Tests.csproj ax.secure.dataManagement.integration.Tests/
COPY ax.encryptionProvider/ax.encryptionProvider.csproj ax.encryptionProvider/
COPY ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj ax.encryptionProvider.Tests/
COPY ax.storage/ax.storage.csproj ax.storage/
COPY ax.storage.Tests/ax.storage.Tests.csproj ax.storage.Tests/

RUN dotnet restore ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj
RUN dotnet restore ax.storage.Tests/ax.storage.Tests.csproj
RUN dotnet restore ax.secure.dataManagement.integration.Tests/ax.secure.dataManagement.integration.Tests.csproj
RUN dotnet restore ax.secure.dataManagement/ax.secure.dataManagement.csproj
COPY . .

RUN dotnet test ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj -c Release
RUN dotnet test ax.storage.Tests/ax.storage.Tests.csproj -c Release
RUN dotnet test ax.secure.dataManagement.integration.Tests/ax.secure.dataManagement.integration.Tests.csproj -c Release

WORKDIR /src/ax.secure.dataManagement
RUN dotnet build ax.secure.dataManagement.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ax.secure.dataManagement.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ax.secure.dataManagement.dll","--server.urls", "http://0.0.0.0:5100"]
