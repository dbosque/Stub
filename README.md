# Stub
Standalone stub service for SOAP/REST.

Return xpath and regularexpression triggered responses to specific request.
* Support for request/response messages over SOAP, REST and a socketconnection.
* Standalone Editor for configuration changes.
* WebApi for configuration changes.
* Standalone service.
* Service can run inside Docker image (windows / linux).
* SQL Server or SQLite database support.
* XSLT support with the request message as input.

## Defaults
Out of the box the service retrieves its data from the dbstub.db SQLite database.
Default endpoints are :
- Socket : 0.0.0.0:8008 
- Rest   : http://*:8081/http
- Soap   : http://*:8081/soap
- Configuration : http://*:8081/configuration

Start the service by running `dotnet dBosque.Stub.Standalone.dll -o`

## How it works
Each message send to one of the endpoinst is parsed to determine the namespace and rootnode.
Based upon the namespace and rootnode a template is located.
In case templates exist for the given namespace and rootnode the complete request message is parsed to see if any xpath expression, as defined in the template, match the request message.
In case a valid template is found, the stored response message will be returned.

Instead of the default HttpStatusCode 200, any other HttpStatusCode can be returned.

## Installation
- Editor + Server : [Installer](https://dbosque.blob.core.windows.net/blog/dBosque.Stub.Editor.msi)
- Server on Docker : [Dockerfile](dBosque.Stub.Standalone/docker/linux-remote/Dockerfile)
  or 
  ` docker build https://github.com/dbosque/Stub.git#master:dBosque.Stub.Standalone\docker\linux-remote -t stub`
   `docker run -p 8008:8008 -p 8081:8081 -v c:\docker\data:/app/data -d -it stub`