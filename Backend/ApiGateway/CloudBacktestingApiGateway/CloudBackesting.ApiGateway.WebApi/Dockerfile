FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["CloudBackesting.ApiGateway.WebApi/CloudBackesting.ApiGateway.WebApi.csproj", "CloudBackesting.ApiGateway.WebApi/"]
RUN dotnet restore "CloudBackesting.ApiGateway.WebApi/CloudBackesting.ApiGateway.WebApi.csproj"
COPY . .
WORKDIR "/src/CloudBackesting.ApiGateway.WebApi"
RUN dotnet build "CloudBackesting.ApiGateway.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CloudBackesting.ApiGateway.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CloudBackesting.ApiGateway.WebApi.dll"]