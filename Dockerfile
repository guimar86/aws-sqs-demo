FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 8989

ENV ASPNETCORE_URLS=http://+:8989

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["AWS.SQS.csproj", "AWS.SQS.csproj"]
RUN dotnet restore "AwsDotnetS3.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "AWS.SQS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AWS.SQS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AWS.SQS.dll"]