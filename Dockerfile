FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY ["Dragon_BlogReal.csproj", ""]
RUN dotnet restore "./Dragon_BlogReal.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Dragon_BlogReal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dragon_BlogReal.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Dragon_BlogReal.dll