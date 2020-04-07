FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR ./Server

COPY ./Beam_intern.csproj .
RUN dotnet restore

COPY ./ .
RUN dotnet publish -c Release -o out

EXPOSE 5000-5001
ENTRYPOINT ["dotnet", "run"]
