# Stage 1
FROM microsoft/aspnetcore-build as build-image

WORKDIR /home/app

COPY ./ZnoApplication/*.sln ./
COPY ./ZnoApplication/*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore

COPY . .

RUN dotnet test ./Tests/Tests.csproj

RUN dotnet publish ./ZnoApi/ZnoApi.csproj -o /publish/

# Stage 2
FROM microsoft/aspnetcore

WORKDIR /publish

COPY --from=build-image /publish .

ENTRYPOINT ["dotnet", "ZnoApi.dll"]