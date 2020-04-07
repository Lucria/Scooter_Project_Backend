FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR ./Server

COPY ./Beam_intern.csproj .
RUN dotnet restore "Beam_intern.csproj"

COPY . .
RUN dotnet build "Beam_intern.csproj" -c Release 

FROM build-env AS publish
RUN dotnet publish "Beam_intern.csproj" -c Release -o /out 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine AS final
WORKDIR app
COPY --from=publish /out .
ENTRYPOINT ["dotnet", "Beam_intern.dll"]