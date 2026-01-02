FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and referenced projects for restore
COPY src/services/AcademyIO.Payments.API/AcademyIO.Payments.API.csproj src/services/AcademyIO.Payments.API/
# Copy referenced sibling project(s) so `dotnet restore` can resolve project references
COPY src/services/AcademyIO.Courses.API/AcademyIO.Courses.API.csproj src/services/AcademyIO.Courses.API/
COPY src/building-blocks/ building-blocks/

RUN dotnet restore src/services/AcademyIO.Payments.API/AcademyIO.Payments.API.csproj

# Copy the rest of the sources
COPY . .

WORKDIR /src/src/services/AcademyIO.Payments.API
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:80

ENTRYPOINT ["dotnet", "AcademyIO.Payments.API.dll"]
