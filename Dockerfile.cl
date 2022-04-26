FROM mcr.microsoft.com/dotnet/sdk
COPY /HTTPClient /HTTPClient
WORKDIR /HTTPClient/
CMD ["./HTTPClient", "50", "1000"]