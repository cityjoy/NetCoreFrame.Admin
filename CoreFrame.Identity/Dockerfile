#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see http://aka.ms/containercompat 

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 3621
EXPOSE 44319

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY CoreFrame.Identity/06.CoreFrame.IdentityServer.csproj CoreFrame.Identity/
COPY CoreFrame.Util/01.CoreFrame.Util.csproj CoreFrame.Util/
RUN dotnet restore CoreFrame.Identity/06.CoreFrame.IdentityServer.csproj
COPY . .
WORKDIR /src/CoreFrame.Identity
RUN dotnet build 06.CoreFrame.IdentityServer.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish 06.CoreFrame.IdentityServer.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "06.CoreFrame.IdentityServer.dll"]
