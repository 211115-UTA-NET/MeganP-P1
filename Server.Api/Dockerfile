FROM mcr.microsoft.com/dotnet/aspnet:6.0 
#address to the image
#FROM docker.io/<user>/<image>:<tag>
#FROM node:12-alpine 

COPY bin/Release/net6.0/publish/ App/
#COPY <relative path from Dockerfile to file(s)/folder> <target location in image>
#COPY - finds the required files in our own file system and we give 
#it the relative path from our docker file to those files. 
#Then we tell it where we want to copy those files to 

WORKDIR /App
#changes the working directory, if the desired directory doesn't exist it makes one

#OR

#FROM mcr.microsoft.com/dotnet/aspnet:6.0
#WORKDIR /App
#COPY bin/Release/net6.0/publish/ .


#you can have as many ENTRYPOINT commands as needed.
#ENTRYPOINT sets the container to be an executable, and specifies what to run on startup
#CMD[] is similar to ENTRYPOINT but you can only use one CMD command
ENTRYPOINT ["dotnet", "Server.Api.dll"]