FROM microsoft/dotnet:2-runtime-jessie
WORKDIR /app
EXPOSE 8080
EXPOSE 8008
ENTRYPOINT ["dotnet", "dBosque.Stub.Standalone.dll" , "-o"]

# Install app
ENV APP_VERSION 1.4
ENV APP_DOWNLOAD_URL https://dbosque.blob.core.windows.net/blog/dBosque.Stub.$APP_VERSION.tar.gz

RUN curl -SL $APP_DOWNLOAD_URL --output app.tar.gz \
	&& mkdir -p /app \
	&& tar -zxf app.tar.gz -C /app \
    && rm app.tar.gz 




#docker commands
#
# Create volume folder 
# --> mkdir c:\docker\
# --> mkdir c:\docker\data
# Create docker image
# --> docker build -f .\Dockerfile-linux-remote -t stub-l . 
# Start container
# --> docker run -p 8008:8008 -p 8081:8081 -v c:\docker\data:/app/data -d -it stub-l
# Show log
# --> docker logs <containerId>
# Attach
# --> docker attach <containerid>
