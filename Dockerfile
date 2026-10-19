FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY wt-lab4-4mvc-popko.csproj ./
RUN dotnet restore ./wt-lab4-4mvc-popko.csproj
COPY . .
RUN dotnet publish ./wt-lab4-4mvc-popko.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_ENVIRONMENT=Production
EXPOSE 10000
ENTRYPOINT ["dotnet", "wt-lab4-4mvc-popko.dll"]
