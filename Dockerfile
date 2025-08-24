# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copia a solução
COPY *.sln ./

# Copia cada projeto separadamente
COPY Autenticacao/*.csproj ./Autenticacao/
COPY Automacao/*.csproj ./Automacao/

# Restaura os pacotes
RUN dotnet restore

# Copia todo o restante do código
COPY . .

# Publica a aplicação
RUN dotnet publish -c Release -o out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENV ASPNETCORE_URLS=http://+:$PORT
ENTRYPOINT ["dotnet", "RNovaTech.dll"]
