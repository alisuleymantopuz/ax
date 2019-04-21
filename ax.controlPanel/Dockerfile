FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 5000

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ax.controlPanel/ax.controlPanel.csproj ax.controlPanel/
COPY ax.fileProcessor/ax.fileProcessor.csproj ax.fileProcessor/
COPY ax.fileProcessor.Tests/ax.fileProcessor.Tests.csproj ax.fileProcessor.Tests/
COPY ax.encryptionProvider/ax.encryptionProvider.csproj ax.encryptionProvider/
COPY ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj ax.encryptionProvider.Tests/

RUN dotnet restore ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj
RUN dotnet restore ax.fileProcessor.Tests/ax.fileProcessor.Tests.csproj
RUN dotnet restore ax.controlPanel/ax.controlPanel.csproj
COPY . .

RUN dotnet test ax.encryptionProvider.Tests/ax.encryptionProvider.Tests.csproj -c Release
RUN dotnet test ax.fileProcessor.Tests/ax.fileProcessor.Tests.csproj -c Release

WORKDIR /src/ax.controlPanel
RUN dotnet build ax.controlPanel.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ax.controlPanel.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ax.controlPanel.dll","--server.urls", "http://0.0.0.0:5000"]
