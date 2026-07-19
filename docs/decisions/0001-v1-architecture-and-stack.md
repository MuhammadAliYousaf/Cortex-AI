# ADR 0001: Adopt Clean Architecture and the v1.0 stack

- **Status:** Accepted
- **Date:** 2026-07-19

## Context

Cortex AI needs a foundation for an ASP.NET Core application that can grow from a basic AI chat experience into capabilities such as prompt engineering, RAG, structured outputs, function calling, memory, and workflows. The application should keep its business behavior testable and avoid coupling it directly to its database, web framework, or a specific AI SDK.

## Decision

Version 1.0 will use:

- ASP.NET Core Razor Pages for the web UI
- Clean Architecture with Domain, Application, Infrastructure, and Web boundaries
- PostgreSQL in Docker Compose for local development
- Entity Framework Core for data access and migrations
- the OpenAI .NET SDK for AI-provider integration
- Bootstrap 5 for UI styling
- Serilog for structured logging
- xUnit for automated tests

## Consequences

- Application and domain logic can be tested without requiring a running web host or database.
- External integrations are isolated in Infrastructure, improving replacement and testability options.
- The initial solution has more projects and dependency boundaries than a single-project application, which adds setup overhead but keeps growth manageable.
- Docker Compose becomes the standard local path for starting PostgreSQL.
