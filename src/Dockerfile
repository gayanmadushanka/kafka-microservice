FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5001
ENV ASPNETCORE_URLS=http://+:5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["Shared/Shared.csproj", "Shared/"]
COPY ["Services.Order/Services.Order.csproj", "Services.Order/"]
RUN dotnet restore "Services.Order/Services.Order.csproj"
COPY . .

WORKDIR "/src/Services.Order"
RUN dotnet build "Services.Order.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Services.Order.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Services.Order.dll", "--server.urls", "http://0.0.0.0:5001"]