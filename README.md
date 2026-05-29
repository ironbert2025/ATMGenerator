# ATMGenerator

A Windows desktop application for generating NinjaTrader ATM (Automated Trade Management) strategy templates from a user-supplied input value.

## Overview

Given a numeric input, ATMGenerator computes stop-loss and target values, names the template accordingly, and saves it as an XML file compatible with NinjaTrader under `C:\Template`.

**Formula:**
```
StopLoss = Ceiling(inputValue / 1.25)
Target   = StopLoss × 3
Name     = "ATM{StopLoss}{Target}"   // e.g., ATM2060
```

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows OS (WinForms UI)
- `C:\Template` directory must exist with write permissions

## Build & Run

```powershell
# Build all projects
dotnet build

# Run the application
dotnet run --project ATMGenerator.UI
```

## Architecture

The solution follows Clean Architecture across four projects:

| Project | Role |
|---|---|
| `ATMGenerator.Domain` | `AtmTemplate` entity (`TemplateName`, `StopLoss`, `Target`, `FilePath`) |
| `ATMGenerator.Application` | Use cases (`GenerateAtmTemplateUseCase`, `GetAtmTemplatesUseCase`) and `IAtmTemplateRepository` interface |
| `ATMGenerator.Infrastructure` | `AtmTemplateXmlRepository` — reads/writes XML files to `C:\Template` |
| `ATMGenerator.UI` | WinForms `MainForm` — wires up the layers directly (no DI container) |

Dependency rule: inner layers have no knowledge of outer layers.

## UI

The main screen displays a **15×10 DataGridView** mapping existing templates by `StopLoss` range:

- Row 0 → StopLoss 10–19
- Row 1 → StopLoss 20–29
- …
- Row 14 → StopLoss 150–159

On startup the grid is populated by reading existing XML filenames from `C:\Template` and parsing the `StopLoss` value from each name.

## Known Issues

- The output path `C:\Template` is hard-coded and must exist before running the application.
