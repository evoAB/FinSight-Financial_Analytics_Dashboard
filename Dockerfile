# Use the official .NET 8.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["FinanceDashboard.csproj", "./"]

RUN dotnet restore "FinanceDashboard.csproj"

# Copy the rest of the files into the container
COPY . .

# Build the app
WORKDIR "/src"
RUN dotnet build "FinanceDashboard.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "FinanceDashboard.csproj" -c Release -o /app/publish

# Use the .NET 8.0 ASP.NET runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the built app from the publish step
COPY --from=publish /app/publish .

# Set the entrypoint
ENTRYPOINT ["dotnet", "FinanceDashboard.dll"]
