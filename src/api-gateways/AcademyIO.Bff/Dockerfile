FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and building-blocks for restore
COPY src/api-gateways/AcademyIO.Bff/AcademyIO.Bff.csproj src/api-gateways/AcademyIO.Bff/
COPY src/building-blocks/ src/building-blocks/

RUN dotnet restore src/api-gateways/AcademyIO.Bff/AcademyIO.Bff.csproj

# Copy everything and publish
COPY . .
WORKDIR /src/src/api-gateways/AcademyIO.Bff
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AcademyIO.Bff.dll"]
