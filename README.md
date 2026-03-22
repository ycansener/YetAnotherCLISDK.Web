# YetAnotherCLISDK.Web

Blazor Server components for [YetAnotherCLISDK](https://github.com/ycansener/YetAnotherCLISDK) — a CLI-like terminal interface for web applications.

[![NuGet](https://img.shields.io/nuget/v/YetAnotherCLISDK.Web)](https://www.nuget.org/packages/YetAnotherCLISDK.Web)
[![License: GNU General Public License v3.0](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

---

## What is this?

`YetAnotherCLISDK.Web` brings the terminal aesthetic of [YetAnotherCLISDK](https://github.com/ycansener/YetAnotherCLISDK) to Blazor Server applications. Every component (Panel, Table, Rule, Lists, TreeView, BarChart, ProgressBar, Spinner, and interactive Menus) is rendered as styled HTML using ANSI-to-HTML conversion — giving your web app the look and feel of a real terminal.

## Repository Structure

```
YetAnotherCLISDK.Web/
├── YetAnotherCLISDK.Web/          # Razor Class Library (NuGet package)
│   ├── Components/                # All Blazor components
│   ├── AnsiRenderer.cs            # ANSI escape code → HTML converter
│   ├── Models.cs                  # Shared model types
│   └── wwwroot/cli-sdk.css        # Terminal theme stylesheet
└── YetAnotherCLISDK.Web.Demo/    # Blazor Server showcase app
    └── Pages/Index.razor          # Full component showcase
```

## Getting Started

**Install the NuGet package:**

```bash
dotnet add package YetAnotherCLISDK.Web
```

**Add the stylesheet to `_Host.cshtml`:**

```html
<link href="_content/YetAnotherCLISDK.Web/cli-sdk.css" rel="stylesheet" />
```

**Add namespaces to `_Imports.razor`:**

```razor
@using YetAnotherCLISDK.Web
@using YetAnotherCLISDK.Web.Components
@using YetAnotherCLISDK.Core
```

**Use components in your pages:**

```razor
<div class="cli-terminal">
    <CliPanel Content="Hello, world!"
              Title="Welcome"
              Border="@BorderStyle.Rounded"
              BorderColor="@(new Style { Foreground = Color.Cyan })" />

    <CliSpinner Style="CliSpinner.CliSpinnerStyle.Dots" Label="Loading..." />
</div>
```

## Components

| Component | Description |
|---|---|
| `<CliPanel>` | Bordered box with optional title and padding |
| `<CliTable>` | Data table with column alignment and header styling |
| `<CliRule>` | Horizontal divider with optional title |
| `<CliBulletList>` | Unordered list with customizable bullet |
| `<CliOrderedList>` | Numbered list with styled numbers |
| `<CliTreeView>` | Recursive tree structure |
| `<CliBarChart>` | Horizontal bar chart with per-bar colors |
| `<CliProgressBar>` | Real-time progress bar with `Advance()` / `SetValue()` |
| `<CliSpinner>` | Animated spinner (6 styles) |
| `<CliSelectionMenu>` | Single-select menu with keyboard navigation |
| `<CliMultiSelectMenu>` | Multi-select menu with Space to toggle |

Full usage examples and parameter reference: [NuGet README](YetAnotherCLISDK.Web/README.md)

## Requirements

- .NET 10+
- Blazor Server
- [YetAnotherCLISDK](https://www.nuget.org/packages/YetAnotherCLISDK) 0.0.2+

## License

MIT
