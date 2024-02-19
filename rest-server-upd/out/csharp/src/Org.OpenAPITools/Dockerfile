FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 8000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/api
COPY Business/ Business/
COPY DataAccess/ DataAccess/
COPY Entities/ Entities/
COPY Core/ Core/
WORKDIR /src/api/rest
COPY src/Org.OpenAPITools/ Org.OpenAPITools/

RUN dotnet restore ./Org.OpenAPITools/Org.OpenAPITools.csproj

RUN dotnet build "./Org.OpenAPITools/Org.OpenAPITools.csproj" -c Release -o out

FROM build AS publish
RUN dotnet publish "./Org.OpenAPITools/Org.OpenAPITools.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Org.OpenAPITools.dll"]