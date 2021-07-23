#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MessageApprover.Commands.Api/MessageApprover.Commands.Api.csproj", "MessageApprover.Commands.Api/"]
COPY ["MessageApprover.Commands.Services.Abstractions/MessageApprover.Commands.Services.Abstractions.csproj", "MessageApprover.Commands.Services.Abstractions/"]
COPY ["MessageApprover.Domain/MessageApprover.Domain.csproj", "MessageApprover.Domain/"]
COPY ["MessageApprover.Commands.Messages/MessageApprover.Saga.Messages.csproj", "MessageApprover.Commands.Messages/"]
COPY ["MessageApprover.Settings/MessageApprover.Settings.csproj", "MessageApprover.Settings/"]
COPY ["MessageApprover.Commands.DataAccess.Mongo/MessageApprover.Commands.DataAccess.Mongo.csproj", "MessageApprover.Commands.DataAccess.Mongo/"]
COPY ["MessageApprover.Commands.DataAccess.Mongo.Abstractions/MessageApprover.Commands.DataAccess.Mongo.Abstractions.csproj", "MessageApprover.Commands.DataAccess.Mongo.Abstractions/"]
COPY ["MessageApprover.Commands.Abstractions/MessageApprover.Commands.Abstractions.csproj", "MessageApprover.Commands.Abstractions/"]
COPY ["MessageApprover.Commands/MessageApprover.Commands.csproj", "MessageApprover.Commands/"]
COPY ["MessageApprover.Commands.Services/MessageApprover.Commands.Services.csproj", "MessageApprover.Commands.Services/"]
RUN dotnet restore "MessageApprover.Commands.Api/MessageApprover.Commands.Api.csproj"
COPY . .
WORKDIR "/src/MessageApprover.Commands.Api"
RUN dotnet build "MessageApprover.Commands.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageApprover.Commands.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageApprover.Commands.Api.dll"]
