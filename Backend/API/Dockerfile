
# Usando a imagem necessária a execução e nomeando como build-env
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Gerar uma pasta no ambiente virtual
WORKDIR /app

# Copiar tudo na pasta para o ambiente virtual
COPY . ./

# Baixar todas as dependencias necessarias (se houver mais de um csproj na solution necessário especificar o projeto)
RUN dotnet restore PersonTransaction.csproj

#criar a pasta de data e rodar o entityframework para integrar o banco de dados

# Publicar no ambiente virtual /app/out
RUN dotnet publish "./PersonTransaction.csproj" -c Release -o out

# Gerar novo ambiente e nomeando como runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Gerar uma pasta no ambiente virtual
WORKDIR /runtime-app

# Copiar tudo do ambiente build-env (--from=nome-ambiente /pasta-origem pasta-destino)
COPY --from=build-env /app/out .

# Porta a ser exposta
EXPOSE 8080
# Codigo a ser executado para rodar a imagem
ENTRYPOINT ["dotnet", "PersonTransaction.dll"]