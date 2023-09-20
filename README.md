# Reactris-backend
Backend for [reactris](https://github.com/kiipuri/reactris)

## Running
### With docker compose
```
docker compose up
```

### Natively
Dependencies:
- PostgreSQL
- .NET 7.0 SDK
- .NET 7.0 Runtime
- ASP.NET Core 7.0

1. Update server to localhost in appsettings.json
2. Run PostgreSQL and create database and user as defined in appsettings.json
3. Install Entity Framework Tools
```
dotnet tool install --global dotnet-ef
```
5. Update database
```bash
dotnet ef database update
```
6. Run the app
```
dotnet run
```
