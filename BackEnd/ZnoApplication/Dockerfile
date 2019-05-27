# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-image

WORKDIR /home/app

COPY ./*.sln ./
COPY ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . .

RUN dotnet test ./Tests/Tests.csproj

RUN dotnet publish ./ZnoApi/ZnoApi.csproj -o /publish/

# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS runtime

WORKDIR /publish

COPY --from=build-image /publish .

ENTRYPOINT ["dotnet", "ZnoApi.dll"]
