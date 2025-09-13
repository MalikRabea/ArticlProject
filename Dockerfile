# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY ArticlPro/*.csproj ./ArticlPro/
COPY ArticlPro.Core/*.csproj ./ArticlPro.Core/
COPY ArticlPro.Data/*.csproj ./ArticlPro.Data/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/ArticlPro
RUN dotnet publish -c Release -o /app/publish --no-restore

# Stage 2: Run the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80

# Start the application
ENTRYPOINT ["dotnet", "ArticlPro.dll"]
