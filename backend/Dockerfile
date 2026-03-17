FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY backend/Vietravel.sln backend/Vietravel.sln
COPY backend/src backend/src

WORKDIR /src/backend
RUN dotnet restore Vietravel.sln
RUN dotnet publish src/Vietravel.Tours.Api/Vietravel.Tours.Api.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:5141
EXPOSE 5141

ENTRYPOINT ["dotnet", "Vietravel.Tours.Api.dll"]

