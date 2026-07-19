# Cortex AI

Cortex AI is an AI engineering playground built with ASP.NET Core to explore modern large language model (LLM) capabilities. It will evolve from a simple AI chat application into a production-ready platform featuring prompt engineering, retrieval-augmented generation (RAG), structured outputs, function calling, memory, and AI workflows.

## Status

Version 1.0 is in initial development on the `v1.0` branch. The solution structure and application features have not yet been created.

## Planned technology stack

- ASP.NET Core Razor Pages
- Clean Architecture
- PostgreSQL via Docker Compose
- Entity Framework Core
- Repository and Specification patterns
- OpenAI .NET SDK
- Bootstrap 5
- Serilog
- xUnit

## Repository guide

- `AGENTS.md` contains project-specific working conventions for AI and human contributors.
- `docs/architecture.md` describes the intended architecture and its boundaries.
- `docs/decisions/` records important technical decisions.

## Getting started

1. Copy `.env.example` to `.env` and replace the example passwords.
2. Start the application and PostgreSQL with `docker compose up --build`.
3. Open `http://localhost:8080` and sign in at `/Identity/Account/Login` with the default admin email and password configured in `.env`.

For local development outside Docker, set `ConnectionStrings__CortexAI`, `DefaultAdmin__Email`, and `DefaultAdmin__Password` in your environment or user secrets before running `dotnet run --project src/CortexAI.Web`.

The database schema is managed with EF Core migrations. The application applies pending migrations at startup, then creates the configured administrator account if it does not exist.
