# Stage 1
FROM  microsoft/aspnetcore-build  AS build-image

WORKDIR /home/app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . .

#RUN dotnet test ./Tests/Tests.csproj

RUN dotnet publish ./ZnoApi/Zno.Server.csproj -o /publish/

# Stage 2
#FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime
FROM microsoft/aspnetcore AS runtime

WORKDIR /publish

COPY --from=build-image /publish .

ENTRYPOINT ["dotnet", "Zno.Server.dll"]
