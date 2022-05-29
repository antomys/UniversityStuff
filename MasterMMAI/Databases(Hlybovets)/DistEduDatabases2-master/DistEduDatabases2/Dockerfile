FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DistEduDatabases2/DistEduDatabases2.csproj", "DistEduDatabases2/"]
RUN dotnet restore "DistEduDatabases2/DistEduDatabases2.csproj"
COPY . .
WORKDIR "/src/DistEduDatabases2"
RUN dotnet build "DistEduDatabases2.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DistEduDatabases2.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DistEduDatabases2.dll"]
