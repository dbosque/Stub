FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 8080
EXPOSE 8008
ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "dBosque.Stub.Server.dll" , "-o"]

COPY bin/Docker/publish /app


