FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY 4erp.api/*.csproj ./4erp.api/
RUN dotnet restore ./4erp.api/4erp.api.csproj

COPY . .
WORKDIR /src/4erp.api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "4erp.api.dll"]
