FROM microsoft/dotnet:2.0-runtime
WORKDIR /app
EXPOSE 8080
EXPOSE 8008
ENV ASPNETCORE_URLS http://*:8080
ENTRYPOINT ["dotnet", "dBosque.Stub.Server.dll" , "-o"]

COPY bin/Docker/publish /app


