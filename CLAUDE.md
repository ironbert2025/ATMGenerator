# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build & Run

```powershell
# Build all projects
dotnet build

# Run the application
dotnet run --project ATMCreator.UI
```

There are no test projects and no linting configuration.

## Architecture

ATMCreator follows Clean Architecture with four projects, each with a strict dependency rule (inner layers have no knowledge of outer layers):

- **ATMCreator.Domain** — `AtmTemplate` entity only (`TemplateName`, `StopLoss`, `Target`, `FilePath`). No dependencies.
- **ATMCreator.Application** — Use cases (`GenerateAtmTemplateUseCase`, `GetAtmTemplatesUseCase`) and the `IAtmTemplateRepository` interface. Depends on Domain only.
- **ATMCreator.Infrastructure** — `AtmTemplateXmlRepository` implements `IAtmTemplateRepository`. Writes/reads XML files to `C:\Template`. Depends on Domain + Application.
- **ATMCreator.UI** — WinForms `MainForm`. Wires up use cases by instantiating the infrastructure repository directly (no DI container). Depends on all layers.

## Core Business Logic

Given a user input value, the use case computes:
```
StopLoss = Ceiling(inputValue / 1.25)
Target   = StopLoss × 3
Name     = "ATM{StopLoss}{Target}"   // e.g., ATM2060
```

Templates are saved as NinjaTrader-formatted XML files (`{Name}.xml`) under `C:\Template`.

## UI Grid Layout

The 15×10 `DataGridView` maps templates by `StopLoss` range: row 0 = range 10–19, row 1 = range 20–29, …, row 14 = range 150–159. Columns represent individual templates within each row's range. On startup, `GetAtmTemplatesUseCase` reads existing XML filenames from disk and parses the `StopLoss` value out of the name to place each template in the correct cell.

## Known Issues

- The output path `C:\Template` is hard-coded in `AtmTemplateXmlRepository` and requires that directory to exist with write permissions.
