FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 4477
EXPOSE 44347

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Money.Accounts.Api.csproj .
RUN dotnet restore Money.Accounts.Api.csproj
COPY . .
WORKDIR /src
RUN dotnet build Money.Accounts.Api.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Money.Accounts.Api.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Money.Accounts.Api.dll"]
