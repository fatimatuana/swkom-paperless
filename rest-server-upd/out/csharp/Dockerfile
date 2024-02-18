FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app
EXPOSE 8000
# Copy the main solution file
#COPY ../../Org.OpenAPITools.sln ./

# Copy the Business.csproj file and restore dependencies
#WORKDIR /src


COPY Business/Business.csproj Business/
COPY DataAccess/DataAccess.csproj DataAccess/
COPY Entities/Entities.csproj Entities/
COPY Core/Core.csproj Core/
RUN dotnet restore "Org.OpenAPITools/Org.OpenAPITools.csproj"
COPY . .
WORKDIR "/src/Org_OpenAPITools"
RUN dotnet build "Org.OpenAPITools.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Org.OpenAPITools.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Org.OpenAPITools.dll"]