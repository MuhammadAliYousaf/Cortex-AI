# Cortex AI contributor guide

## Project status

Active development happens on the `v1.0` branch. The project is in its initial setup phase; do not describe planned capabilities as implemented.

## v1.0 technology stack

- ASP.NET Core Razor Pages
- Clean Architecture
- PostgreSQL, run locally with Docker Compose
- Entity Framework Core
- OpenAI .NET SDK
- Bootstrap 5
- Serilog
- xUnit
- Docker Compose

## Working conventions

- Keep domain logic independent of infrastructure, web UI, database, and SDK concerns.
- Use Repository and Specification patterns for persistence access. Keep repository abstractions in the core layer and EF Core implementations in Infrastructure; application use cases must not query `DbContext` directly.
- Keep secrets out of source control. Use environment variables and provide safe names in `.env.example` when configuration is introduced.
- Add or update xUnit tests for behavior changes where practical.
- Use EF Core migrations for schema changes; never hand-edit a production database.
- Log meaningful application events with Serilog, without logging secrets, access tokens, or sensitive prompt content.
- Prefer Bootstrap 5 utilities and components before introducing custom UI libraries.
- Keep Docker Compose configuration suitable for a local development environment.

## Validation

When relevant, run the applicable commands before handing work off:

```powershell
dotnet build
dotnet test
docker compose config
```

## Documentation

- Keep `README.md` current with setup and run instructions.
- Record significant, lasting technical choices under `docs/decisions/`.
- Keep `docs/architecture.md` aligned with the implemented solution structure.
