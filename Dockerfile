FROM node:24-alpine AS frontend-build
WORKDIR /app/frontend
COPY frontend/package*.json ./
RUN npm ci
COPY frontend/ ./
RUN npm run build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS api-build
WORKDIR /src
COPY src/PetCare.Api/PetCare.Api.csproj src/PetCare.Api/
RUN dotnet restore src/PetCare.Api/PetCare.Api.csproj
COPY src/PetCare.Api/ src/PetCare.Api/
RUN dotnet publish src/PetCare.Api/PetCare.Api.csproj -c Release -o /out
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
COPY --from=api-build /out .
COPY --from=frontend-build /app/frontend/dist ./wwwroot
USER $APP_UID
ENTRYPOINT ["dotnet", "PetCare.Api.dll"]
