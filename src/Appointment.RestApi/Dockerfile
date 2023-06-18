FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Appointment.RestApi/Appointment.RestApi.csproj", "Appointment.RestApi/"]
RUN dotnet restore "Appointment.RestApi/Appointment.RestApi.csproj"
COPY . .
WORKDIR "/src/Appointment.RestApi"
RUN dotnet build "Appointment.RestApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Appointment.RestApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Appointment.RestApi.dll"]
