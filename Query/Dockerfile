FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore

# Copy everything else and build
COPY . ./
#RUN dotnet restore
RUN dotnet restore "Presentation/Query.Api/Query.Api.csproj" 
WORKDIR "Presentation/Query.Api"
RUN dotnet publish -c Release -o /app/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_ENVIRONMENT="Development"
ENTRYPOINT ["dotnet", "Query.Api.dll"]