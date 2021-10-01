FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore "Services.Payment.csproj"
RUN dotnet publish "Services.Payment.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 5004
ENV ASPNETCORE_URLS=http://+:5004
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Services.Payment.dll", "--server.urls", "http://0.0.0.0:5004"]