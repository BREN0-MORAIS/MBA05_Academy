# --------------------------
# 1. Base runtime
# --------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Define URLs padr�o para Kestrel
ENV ASPNETCORE_URLS=http://+:80;https://+:443
ENV DOTNET_RUNNING_IN_CONTAINER=true

# --------------------------
# 2. Build
# --------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia solution e projetos
COPY *.sln ./
COPY src/ src/

# Restaura depend�ncias
RUN dotnet restore "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj"

# Build
RUN dotnet build "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj" -c Release -o /app/build

# --------------------------
# 3. Publish
# --------------------------
FROM build AS publish
RUN dotnet publish "src/services/AcademyIO.Auth.API/AcademyIO.Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --------------------------
# 4. Final
# --------------------------
FROM base AS final
WORKDIR /app

# Copia arquivos publicados da build
COPY --from=publish /app/publish .

# Define entrypoint
ENTRYPOINT ["dotnet", "AcademyIO.Auth.API.dll"]
