FROM microsoft/dotnet:2-runtime-jessie
WORKDIR /app
EXPOSE 8080
EXPOSE 8008
ENTRYPOINT ["dotnet", "dBosque.Stub.Server.dll" , "-o"]
COPY bin/Docker/publish /app


#docker commands
#
# Create volume folder 
# --> mkdir c:\docker\
# --> mkdir c:\docker\data
# Create docker image
# --> docker build -f .\Dockerfile-linux -t stub-l . 
# Start container
# --> docker run -p 8008:8008 -p 8081:8081 -v c:\docker\data:/app/data -d -it stub-l
# Show log
# --> docker logs <containerId>
# Attach
# --> docker attach <containerid>
