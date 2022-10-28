FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/SarveenTech.SmartCattle.BackendSample.Gateway/SarveenTech.SmartCattle.BackendSample.Gateway.csproj", "src/SarveenTech.SmartCattle.BackendSample.Gateway/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.MySql/IdentityServer4.Admin.EntityFramework.MySql.csproj", "src/IdentityServer4.Admin.EntityFramework.MySql/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.Identity/IdentityServer4.Admin.EntityFramework.Identity.csproj", "src/IdentityServer4.Admin.EntityFramework.Identity/"]
COPY ["src/IdentityServer4.Admin.EntityFramework/IdentityServer4.Admin.EntityFramework.csproj", "src/IdentityServer4.Admin.EntityFramework/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.Extensions/IdentityServer4.Admin.EntityFramework.Extensions.csproj", "src/IdentityServer4.Admin.EntityFramework.Extensions/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.Shared/IdentityServer4.Admin.EntityFramework.Shared.csproj", "src/IdentityServer4.Admin.EntityFramework.Shared/"]
COPY ["src/IdentityServer4.Admin.BusinessLogic/IdentityServer4.Admin.BusinessLogic.csproj", "src/IdentityServer4.Admin.BusinessLogic/"]
COPY ["src/IdentityServer4.Admin.BusinessLogic.Shared/IdentityServer4.Admin.BusinessLogic.Shared.csproj", "src/IdentityServer4.Admin.BusinessLogic.Shared/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.PostgreSQL/IdentityServer4.Admin.EntityFramework.PostgreSQL.csproj", "src/IdentityServer4.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/IdentityServer4.Admin.BusinessLogic.Identity/IdentityServer4.Admin.BusinessLogic.Identity.csproj", "src/IdentityServer4.Admin.BusinessLogic.Identity/"]
COPY ["src/IdentityServer4.Admin.EntityFramework.SqlServer/IdentityServer4.Admin.EntityFramework.SqlServer.csproj", "src/IdentityServer4.Admin.EntityFramework.SqlServer/"]
COPY ["src/IdentityServer4.Shared/IdentityServer4.Shared.csproj", "src/IdentityServer4.Shared/"]
RUN dotnet restore "src/SarveenTech.SmartCattle.BackendSample.Gateway/SarveenTech.SmartCattle.BackendSample.Gateway.csproj"
COPY . .
WORKDIR "/src/src/SarveenTech.SmartCattle.BackendSample.Gateway"
RUN dotnet build "SarveenTech.SmartCattle.BackendSample.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SarveenTech.SmartCattle.BackendSample.Gateway.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "SarveenTech.SmartCattle.BackendSample.Gateway.dll"]
