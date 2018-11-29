FROM microsoft/dotnet:2.1-runtime-alpine

# application, runtime & dependency files should be in different layers
COPY publish/ /app/
COPY app/ /app/

WORKDIR /app
ENTRYPOINT ["dotnet", "Ryanair.Reservation.dll"]