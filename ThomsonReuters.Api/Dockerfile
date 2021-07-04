#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ThomsonReuters.Api/ThomsonReuters.Api.csproj", "ThomsonReuters.Api/"]
COPY ["ThomsonReuters.CrossCutting/ThomsonReuters.CrossCutting.csproj", "ThomsonReuters.CrossCutting/"]
COPY ["ThomsonReuters.Infra/ThomsonReuters.Infra.csproj", "ThomsonReuters.Infra/"]
COPY ["ThomsonReuters.Business/ThomsonReuters.Business.csproj", "ThomsonReuters.Business/"]
COPY ["ThomsonReuters.Application/ThomsonReuters.Application.csproj", "ThomsonReuters.Application/"]
RUN dotnet restore "ThomsonReuters.Api/ThomsonReuters.Api.csproj"
COPY . .
WORKDIR "/src/ThomsonReuters.Api"
RUN dotnet build "ThomsonReuters.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ThomsonReuters.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ThomsonReuters.Api.dll"]