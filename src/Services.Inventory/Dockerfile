FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore "Services.Inventory.csproj"
RUN dotnet publish "Services.Inventory.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 5003
ENV ASPNETCORE_URLS=http://+:5003
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Services.Inventory.dll", "--server.urls", "http://0.0.0.0:5003"]