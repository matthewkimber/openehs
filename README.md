# OpenEHS

An open Electronic Healthcare System — a web application for managing hospital patient records, originally built for a hospital in Ghana (KBTH / MLKMC).

It covers patient registration and search, check-ins, charting and vitals, allergies, immunizations, medications, problems/diagnoses, billing and invoicing, and reporting.

## Status

This is a legacy application targeting **.NET 4.0 / ASP.NET MVC 2** and **Visual Studio 2010**, with a **MySQL** database accessed through **NHibernate**. The toolchain is Windows-only.

## Tech stack

| Layer        | Technology                                          |
| ------------ | --------------------------------------------------- |
| Web          | ASP.NET MVC 2 (WebForms `.aspx`/`.ascx` views)      |
| ORM          | NHibernate (XML `.hbm.xml` mappings) + ICriteria    |
| Database     | MySQL                                               |
| Auth         | Forms auth + custom Membership/Role/Profile providers |
| Logging      | NLog                                                |
| Tests        | NUnit 2.5 + Moq                                     |
| Build        | MSBuild (`openehs.build`)                           |

## Repository layout

```
code/        The Visual Studio solution and all buildable code
  src/app/     Application projects (Domain, Data, Infrastructure, Web)
  src/tests/   NUnit test projects
  sql/         Database creation script + setup instructions
  lib/         Vendored third-party DLLs (NHibernate, MySQL connector, Moq, ...)
  packages/    NuGet packages (NLog, NUnit, elmah, ...)
design/      ERDs, UML, and UI mockups
doc/         Project documentation (charter, deployment, test plan, SSRS, generated help)
fixes/       One-off SQL data-repair scripts
```

### Project structure (`code/src/app/`)

The solution is layered, with dependencies flowing inward toward the domain:

- **OpenEhs.Domain** — plain entity classes and enums, grouped by aggregate (Patient, Billing, Charting, etc.). No framework or persistence references.
- **OpenEhs.Data** — NHibernate persistence: `*.hbm.xml` mappings, repositories over `ISession`, and the request-scoped `UnitOfWork`.
- **OpenEhs.Infrastructure.Security** — custom ASP.NET membership/role/profile providers backed by domain entities.
- **OpenEhs.Infrastructure.Logging** — thin logging abstraction over NLog.
- **OpenEhs.Web** — ASP.NET MVC controllers, view models, and views.

## Getting started

### Prerequisites

- Windows with Visual Studio 2010 (or MSBuild for .NET 4.0)
- MySQL server
- IIS or the Visual Studio development web server

### Database setup

Follow `code/sql/First Time Run Instructions.txt`:

1. Create the database and the `OpenEHS_admin` user.
2. Run `code/sql/OpenEHS Database Full Script.sql` (schema, triggers, stored procedures, and seed data).

The connection string lives in `code/src/app/OpenEhs.Web/hibernate.cfg.xml` and defaults to:

```
Server=127.0.0.1;Database=openehs_database;Uid=OpenEHS_admin;Pwd=password;
```

### Build

```bat
cd code
build.bat
```

This runs `msbuild openehs.build`, which cleans, initializes, and compiles the solution into `code/build/`.

### Test

Tests run with NUnit against the built test assemblies, e.g.:

```bat
packages\NUnit.2.5.7.10213\tools\nunit-console.exe build\OpenEhs.Data.Tests.dll
```

> **Note:** the `OpenEhs.Data.Tests` repository tests are integration tests — they connect to a live MySQL database and expect it to be seeded with the script above.

## Architecture notes

- **No DI container.** Controllers instantiate repositories directly (`new PatientRepository()`). The NHibernate session is managed by the static `UnitOfWork` class and scoped per request in `Global.asax.cs` (started on `BeginRequest`, flushed and cleared on `EndRequest`).
- **Authorization** is role-based via `[Authorize(Roles = "...")]` on controllers (e.g. `OPDClerks`, `OPDAdministrators`, `Administrators`).
- **Localization** is forced to `en-GB` with the Ghana cedi (`GH₵`) currency symbol on each request.

For deeper guidance when working in the code, see [CLAUDE.md](CLAUDE.md).

## Authors

See [code/AUTHORS](code/AUTHORS).

## License

Licensed under the Apache License, Version 2.0 — see [LICENSE](LICENSE).
