# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los archivos del proyecto y restaurar dependencias
COPY *.sln .
COPY ["ERP/ERP.csproj", "ERP/"]
RUN dotnet restore "ERP/ERP.csproj"

# Copiar el resto del código y compilar
COPY . .
WORKDIR "/src/ERP"
RUN dotnet publish "ERP.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Exponer puerto (ajústalo si tu app corre en otro puerto)
EXPOSE 5000

# Ejecutar aplicación
ENTRYPOINT ["dotnet", "ERP.dll"]
