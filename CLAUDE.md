# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## What this is

OpenEHS is an Electronic Healthcare System built as an ASP.NET MVC web application for a hospital in Ghana (originally KBTH / MLKMC). It is a legacy .NET 4.0 / Visual Studio 2010 codebase. Domain coverage includes patient records, check-ins, charting/vitals, allergies, immunizations, medications, problems, billing/invoicing, and reporting.

The solution and all buildable code live under `code/`. The repo root also holds non-code assets: `design/` (ERDs, UML, mockups), `doc/` (Word/SSRS docs, generated Sandcastle help), and `fixes/` (one-off SQL data-repair scripts).

## Build & test

The toolchain is Windows-only (MSBuild + IIS/Cassini + MySQL). It does not build on macOS/Linux.

```bat
cd code
build.bat                  REM wraps: msbuild openehs.build /v:m  -> clean, init, compile to code/build/
msbuild openehs.build /t:clean
msbuild OpenEhs.sln /p:Configuration=Release
```

Tests use **NUnit 2.5** with **Moq**. There is no CLI test target wired into the build; run via the NUnit GUI/console or the VS test runner against the built test assemblies:

```bat
packages\NUnit.2.5.7.10213\tools\nunit-console.exe build\OpenEhs.Data.Tests.dll
REM run a single fixture/test:
nunit-console.exe build\OpenEhs.Data.Tests.dll /run:OpenEhs.Data.Tests.Repositories.PatientRepositoryTest
```

Note: repository tests (e.g. `OpenEhs.Data.Tests`) call `UnitOfWork.Start()` and hit a **live MySQL database** — they are integration tests, not isolated units, and require the DB below to be running and seeded.

## Database setup

NHibernate connects to MySQL configured in `code/src/app/OpenEhs.Web/hibernate.cfg.xml` (default: `Server=127.0.0.1;Database=openehs_database;Uid=OpenEHS_admin;Pwd=password`). First-time setup is in `code/sql/First Time Run Instructions.txt`: create the DB/user, then run `code/sql/OpenEHS Database Full Script.sql` (schema + triggers + stored procs + seed data). The MySQL ERD is `code/sql/MySql ERD.mwb`.

## Architecture

The solution is a layered DDD-style design. Dependencies flow inward toward Domain. Projects under `code/src/app/`:

- **OpenEhs.Domain** — POCO entities and enums only, grouped by aggregate under `Model/` (Patient, Billing, Charting, CheckIn, Note, Product, Service, Staff, Surgery, Templates, User, Vitals). All entities implement `IEntity` (`Model/Common Interfaces/IEntity.cs`). No persistence or framework references here.
- **OpenEhs.Data** — NHibernate persistence. Two key pieces:
  - **Mappings** are XML `*.hbm.xml` files in `Mappings/` (embedded resources), **not** Fluent/attribute mappings. Adding or changing a persisted property means editing the matching `.hbm.xml`.
  - **Repositories** (`Repositories/`, interfaces in `Interfaces/`) wrap NHibernate `ISession` and use the **ICriteria** API for queries (e.g. `PatientRepository`).
- **OpenEhs.Infrastructure.Security** — custom ASP.NET providers (`OpenEhsMembershipProvider`, `OpenEhsRoleProvider`, `OpenEhsProfileProvider`) backed by the User/Role domain entities. Wired up in `Web.config`.
- **OpenEhs.Infrastructure.Logging** — thin `ILogger`/`ILogManager` abstraction over NLog (`NLog.config`), with `Fakes/` for testing.
- **OpenEhs.Web** — ASP.NET MVC 2 front end: `Controllers/`, `Models/` (view models), `Views/` (WebForms `.aspx`/`.ascx`), `Helpers/`.

### Unit of Work / session lifecycle (important)

There is **no DI container**. The `UnitOfWork` static class (`OpenEhs.Data/Common/UnitOfWork.cs`) manages the NHibernate session. The session is request-scoped via `Global.asax.cs`:

- `Application_BeginRequest` → `UnitOfWork.Start()` if not already started.
- `Application_EndRequest` → `CurrentSession.Flush()` then `Clear()`.

Repositories read the ambient session through `UnitOfWork.CurrentSession`. Controllers instantiate repositories **directly** in their constructor (e.g. `new PatientRepository()`) against the repository interface — there is no injection. Follow this existing pattern rather than introducing DI.

### Web conventions

- Routing is defined in `Global.asax.cs` (`RegisterRoutes`): default route is `Dashboard/Index`; there are special routes for `Admin/User/...` and paged URLs (`{controller}/{action}/Page{n}`).
- Authorization is role-based via `[Authorize(Roles = "...")]` on controllers (roles like `OPDClerks`, `OPDAdministrators`, `Administrators`). Auth is Forms auth + the custom membership/role providers.
- `BeginRequest` forces culture to **en-GB** with currency symbol `GH₵` (Ghana cedi) — keep formatting locale-aware accordingly.

## Conventions

- Source files carry a header comment block (Project / Group: Ghana Team / Date / Author). Match it when creating new files in `OpenEhs.Data`/`OpenEhs.Domain`.
- Third-party libs are vendored: hand-managed DLLs in `code/lib/` (NHibernate, MySQL connector, Moq, nhprof) plus NuGet `code/packages/` (NLog, NUnit, elmah, Dymo.Printer). There is no `dotnet restore`; references are file paths.
