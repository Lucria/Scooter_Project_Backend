FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build-env
WORKDIR /src

COPY ./Beam_intern.csproj .
RUN dotnet restore

COPY ./ .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0
WORKDIR /src
COPY --from=build-env /src/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Beam_intern.dll"]
