FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["src/TMenos3.Courses.NetCore.DistributedCache.API/TMenos3.Courses.Caching.API.csproj", "src/TMenos3.Courses.NetCore.DistributedCache.API/"]
RUN dotnet restore "src/TMenos3.Courses.NetCore.DistributedCache.API/TMenos3.Courses.NetCore.DistributedCache.API.csproj"
COPY . .
WORKDIR "/src/src/TMenos3.Courses.NetCore.DistributedCache.API"
RUN dotnet build "TMenos3.Courses.NetCore.DistributedCache.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TMenos3.Courses.NetCore.DistributedCache.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TMenos3.Courses.NetCore.DistributedCache.API.dll"]
