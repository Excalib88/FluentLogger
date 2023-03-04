FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FluentLogger.Web/FluentLogger.Web.csproj", "FluentLogger.Web/"]
RUN dotnet restore "FluentLogger.Web/FluentLogger.Web.csproj"
COPY . .
WORKDIR "/src/FluentLogger.Web"
RUN dotnet build "FluentLogger.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FluentLogger.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FluentLogger.Web.dll"]
