FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and referenced projects for restore
COPY src/services/AcademyIO.Students.API/AcademyIO.Students.API.csproj src/services/AcademyIO.Students.API/
COPY src/building-blocks/ building-blocks/

RUN dotnet restore src/services/AcademyIO.Students.API/AcademyIO.Students.API.csproj

# Copy the rest of the sources
COPY . .

WORKDIR /src/src/services/AcademyIO.Students.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AcademyIO.Students.API.dll"]
