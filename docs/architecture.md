# Architecture

## Purpose

Cortex AI will be a web application for experimenting with and building LLM-powered capabilities. Version 1.0 uses ASP.NET Core Razor Pages and follows Clean Architecture so that core business rules stay independent from the web UI, database, and external AI provider.

## Intended solution boundaries

```text
Web (Razor Pages)
        |
Application
        |
Domain
        ^
Infrastructure (EF Core, PostgreSQL, OpenAI SDK, Serilog integrations)
```

### Domain

Contains the core model, business rules, and abstractions that do not depend on frameworks or external services.

### Application

Contains use cases, orchestration, validation, and interfaces required by the domain-facing workflows. It depends on Domain, but not on Web or Infrastructure implementations.

### Infrastructure

Implements external concerns, including PostgreSQL persistence through EF Core and AI-provider integrations through the OpenAI .NET SDK. It depends on Application and Domain.

### Web

Hosts Razor Pages, Bootstrap 5 UI assets, dependency-injection composition, and HTTP concerns. It depends on Application and uses Infrastructure through registration at the composition root.

## Cross-cutting concerns

- **Persistence:** PostgreSQL in Docker for local development; schema changes use EF Core migrations.
- **Configuration:** environment-based configuration. Secrets, including OpenAI API keys and database credentials, must not be committed.
- **Logging:** Serilog provides structured application logs while excluding secrets and sensitive content.
- **Testing:** xUnit will cover domain and application behavior, with integration tests added where they provide value.

## Current state

This document is an architectural baseline. The solution projects, database schema, Compose file, and application features are not implemented yet.
