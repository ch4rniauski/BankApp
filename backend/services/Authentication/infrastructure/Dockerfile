FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . .
RUN dotnet publish -c Release -o /app/publish 

FROM mrc.microsoft.com/dotnet/aspnet:9.0 AS run
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Authentication.Api.dll"]
