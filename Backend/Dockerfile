# Utilise une image de base avec le SDK ASP.NET Core pour la construction
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
# Copie les fichiers de projet et restaure les dépendances
COPY *.sln ./
COPY RailRideBMXHexagonale/*.csproj RailRideBMXHexagonale/
COPY Infrastructure/*.csproj Infrastructure/
COPY Core/*.csproj Core/
COPY Application/*.csproj Application/
COPY TestUnitaire/*.csproj TestUnitaire/
RUN dotnet restore

# Copie le reste du code source et construit l'application
COPY . ./
RUN dotnet publish -c Release -o out

# Utilise une image plus légère pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 80
CMD ["bash", "-c", "sleep 30 && dotnet RailRideBMXHexagonale.dll"]
