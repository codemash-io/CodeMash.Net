FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Isidos.CodeMash.Tests.ServiceLevel/Isidos.CodeMash.Tests.ServiceLevel.csproj", "Isidos.CodeMash.Tests.ServiceLevel/"]
RUN dotnet restore "Isidos.CodeMash.Tests.ServiceLevel/Isidos.CodeMash.Tests.ServiceLevel.csproj"
COPY . .
WORKDIR "/src/Isidos.CodeMash.Tests.ServiceLevel"
RUN dotnet build "Isidos.CodeMash.Tests.ServiceLevel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Isidos.CodeMash.Tests.ServiceLevel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Isidos.CodeMash.Tests.ServiceLevel.dll"]
