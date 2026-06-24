# OpenEHS — Code

This directory contains the Visual Studio solution and all buildable source for OpenEHS, an Electronic Healthcare System targeting .NET 4.0 / ASP.NET MVC 2 with a MySQL database accessed through NHibernate.

## Layout

| Path             | Contents                                                            |
| ---------------- | ------------------------------------------------------------------- |
| `OpenEhs.sln`    | Visual Studio 2010 solution                                         |
| `openehs.build`  | MSBuild script (clean / init / compile to `build/`)                 |
| `build.bat`      | Convenience wrapper: `msbuild openehs.build /v:m`                   |
| `src/app/`       | Application projects (Domain, Data, Infrastructure, Web)            |
| `src/tests/`     | NUnit test projects                                                 |
| `sql/`           | Database creation script and first-time setup instructions          |
| `lib/`           | Vendored third-party DLLs (NHibernate, MySQL connector, Moq, nhprof) |
| `packages/`      | NuGet packages (NLog, NUnit, elmah, Dymo.Printer)                   |
| `AUTHORS`        | Project authors                                                     |
| `LICENSE`        | License pointer (Apache 2.0; full text at repo root)               |

## Build

```bat
build.bat
```

This runs `msbuild openehs.build`, which cleans, initializes, and compiles the solution into `code/build/`.

## Database setup

NHibernate connects to MySQL using the connection string in `src/app/OpenEhs.Web/hibernate.cfg.xml`. For first-time setup, follow `sql/First Time Run Instructions.txt`: create the database and `OpenEHS_admin` user, then run `sql/OpenEHS Database Full Script.sql` (schema, triggers, stored procedures, and seed data).

## Tests

Tests use NUnit 2.5 with Moq. Run them against the built test assemblies, e.g.:

```bat
packages\NUnit.2.5.7.10213\tools\nunit-console.exe build\OpenEhs.Data.Tests.dll
```

> **Note:** the `OpenEhs.Data.Tests` repository tests are integration tests that connect to a live, seeded MySQL database.

For a full project overview and architecture notes, see the [repository README](../README.md) and [CLAUDE.md](../CLAUDE.md).
