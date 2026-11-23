# Platform Console App

A .NET console application that authenticates with an external Platform/Well API, retrieves platform and well records, and synchronizes them into a MySQL database using Entity Framework Core.

## Architecture

```text
Program.cs
  |-- AuthenticationService -- POST login credentials --> External API
  |                            <-- JWT token ------------
  |-- PlatformApiService ---- GET platform/well data --> External API
  |                            Bearer token
  |-- PlatformSyncService --- insert/update -----------> MySQL
								 via AppDbContext / EF Core
```

The application is composed through the .NET Generic Host and dependency injection:

- `Program.cs` configures the MySQL `AppDbContext`, a named `HttpClient` (`ExternalApi`), and the application services. It runs the login, retrieval, and synchronization steps in order.
- `AuthenticationService` reads the API credentials and login endpoint from configuration, sends a JSON login request, and returns the JWT token from the response.
- `PlatformApiService` reads the configured actual or dummy data endpoint, adds the JWT as a Bearer token, and deserializes the response into platform DTOs.
- `PlatformSyncService` finds or creates each platform and its wells, updates their fields, and saves the changes with EF Core.
- `AppDbContext` maps the `Platform` and `Well` entities to the `platforms` and `wells` tables.
- `DTOs` represent API request and response data; `Models` represent persisted database records. Platform and well records include `id`, `uniqueName`, nullable `latitude` and `longitude`, nullable `createdAt` and `updatedAt`, and (for platforms) a `well` collection.
- `Migrations` tracks database schema changes. The migration `MakeLocationAndUpdateFieldsNullable` allows missing coordinates and timestamps; `ReplaceLastUpdateWithCreatedAndUpdatedAt` renames the old timestamp column to `UpdatedAt` and adds `CreatedAt`.

## Configuration

The application loads configuration through the Generic Host from `PlatformConsoleApp\appsettings.json`. Required settings are:

| Setting | Purpose |
| --- | --- |
| `ApiSettings:BaseUrl` | Base address for the external API |
| `ApiSettings:LoginEndpoint` | Login route, relative to the base address |
| `ApiSettings:ActualEndpoint` | Route for retrieving actual platform/well data |
| `ApiSettings:DummyEndpoint` | Route for retrieving dummy platform/well data |
| `ApiCredentials:Username` | API login username |
| `ApiCredentials:Password` | API login password |
| `ConnectionStrings:DefaultConnection` | MySQL connection string |

Do not commit real credentials or production connection strings. Supply secrets through a secure configuration provider (such as .NET user secrets or environment variables) and keep local settings private. Environment variable names can use double underscores, for example `ApiCredentials__Username` and `ConnectionStrings__DefaultConnection`.

## Build and run

Run commands from the `PlatformConsoleApp` project directory:

```powershell
dotnet restore
dotnet build
dotnet run
```

On startup, the app logs in, fetches actual platform/well data, and synchronizes each returned platform and its wells. `PlatformApiService` also exposes a method for retrieving the dummy dataset.

## Database migrations

Migrations are not automatically applied by the application. Apply them to the same database configured by `ConnectionStrings:DefaultConnection` before running against a new or changed schema:

```powershell
dotnet ef database update --project .\PlatformConsoleApp.csproj
```

Apply all pending migrations for the configured database before running the app. The timestamp migration preserves the former `LastUpdate` values by renaming that column to `UpdatedAt`; newly introduced `CreatedAt` values are nullable. Verify that migrations were applied to the exact database used by the running application.

## Error handling

HTTP failures are reported as API errors by the main program; other exceptions are reported as application errors. API service diagnostics include the request URL, response status, and response body for failed data requests. Avoid logging credentials or the JWT token.
