# TaskManager

Trello-style task manager. .NET 10 backend (Clean Architecture), MAUI desktop/mobile client, Angular web client.

## Layout

```
TaskManager/
  TaskManager.sln
  Directory.Build.props
  src/
    TaskManager.Domain/         (classlib, no refs)
    TaskManager.Application/    (classlib, refs Domain)
    TaskManager.Infrastructure/ (classlib, refs Application + Domain)
    TaskManager.Api/            (web, refs Application + Infrastructure)
    TaskManager.Shared/         (classlib, no refs)
    TaskManager.Maui/           (maui, refs Domain + Shared)
  docker/
    Dockerfile.api
    docker-compose.yml
```

## Build

```
dotnet build C:\Users\julia\RiderProject\TaskManager\TaskManager.sln
```

The MAUI Android target requires the `maui-android` workload (not installed in Phase 1). Build only the Windows target with:

```
dotnet build -f net10.0-windows C:\Users\julia\RiderProject\TaskManager\src\TaskManager.Maui\TaskManager.Maui.csproj
```

## Run API

```
dotnet run --project C:\Users\julia\RiderProject\TaskManager\src\TaskManager.Api
```

Swagger UI is available at `http://localhost:PORT/swagger`. The Angular SPA will be served from `wwwroot/` once Phase 3 produces the build artifacts.

## Angular web client

Lives outside this repo at `C:\Users\julia\projects\taskmanager-web`. Phase 1 leaves `wwwroot\` with a placeholder; the Angular build will populate it in Phase 3. To trigger the in-process Angular build from the .csproj, build with `dotnet build /p:BuildAngular=true`.

## Docker

```
docker compose -f C:\Users\julia\RiderProject\TaskManager\docker\docker-compose.yml up --build
```

Exposes API on `http://localhost:8080`. SQLite database is persisted to the `taskdata` volume at `/data/tasks.db`.
