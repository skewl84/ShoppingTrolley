#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /app
COPY . ./

FROM build AS publish

# Test Application
WORKDIR /app/Tests/ShoppingTrolley.Application.UnitTests
RUN dotnet test

# Build app
WORKDIR /app/Src/ShoppingTrolley.API
RUN dotnet publish "ShoppingTrolley.API.csproj" -c Release -o /app/publish

# Build runtime image
FROM runtime-env AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingTrolley.API.dll"]