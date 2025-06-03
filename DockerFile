# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# Use the SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["FinanceDashboard.csproj", "./"]
RUN dotnet restore "FinanceDashboard.csproj"

# Copy all files from the repo into the container
COPY . .
WORKDIR "/src"
RUN dotnet build "FinanceDashboard.csproj" -c Release -o /app/build
RUN dotnet publish "FinanceDashboard.csproj" -c Release -o /app/publish

# Set up the final image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FinanceDashboard.dll"]
