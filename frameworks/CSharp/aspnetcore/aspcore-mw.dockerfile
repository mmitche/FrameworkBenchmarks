FROM microsoft/dotnet:2.2-sdk AS build

WORKDIR /app
COPY Benchmarks .

RUN dotnet publish -c Release -o out
ADD https://github.com/aspnet/Benchmarks/raw/master/src/Benchmarks/testCert.pfx ./out

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS runtime
ENV ASPNETCORE_URLS https://+:8080
ENV COMPlus_ReadyToRun 0
WORKDIR /app
COPY --from=build /app/out ./

RUN openssl version

ENTRYPOINT ["dotnet", "Benchmarks.dll", "scenarios=plaintext"]
