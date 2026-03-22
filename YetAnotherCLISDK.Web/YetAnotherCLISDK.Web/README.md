# YetAnotherCLISDK.Web

Blazor Server components for [YetAnotherCLISDK](https://www.nuget.org/packages/YetAnotherCLISDK) — bring a terminal-like CLI interface to your web applications.

Renders Panel, Table, Rule, Lists, TreeView, BarChart, ProgressBar, Spinner, and interactive Menu components in the browser, styled as a dark terminal UI, powered by ANSI-to-HTML conversion.

---

## Installation

```bash
dotnet add package YetAnotherCLISDK.Web
```

---

## Setup

**1. Add the stylesheet to your `_Host.cshtml`:**

```html
<link href="_content/YetAnotherCLISDK.Web/cli-sdk.css" rel="stylesheet" />
```

**2. Add the namespace to `_Imports.razor`:**

```razor
@using YetAnotherCLISDK.Web
@using YetAnotherCLISDK.Web.Components
@using YetAnotherCLISDK.Core
```

---

## Components

Wrap any component in `<div class="cli-terminal">` for the full terminal look.

### `<CliPanel>`

```razor
<CliPanel Content="Hello, world!"
          Title="Welcome"
          Border="@BorderStyle.Rounded"
          BorderColor="@(new Style { Foreground = Color.Cyan })" />
```

| Parameter | Type | Default | Description |
|---|---|---|---|
| `Content` | `string` | *(required)* | Text content (supports markup) |
| `Title` | `string?` | `null` | Optional panel title |
| `Border` | `BorderStyle?` | `null` | Border style (`Single`, `Double`, `Rounded`, `Heavy`, `Ascii`, `Dotted`) |
| `BorderColor` | `Style?` | `null` | Border color and style |
| `TitleStyle` | `Style?` | `null` | Title color and style |
| `PaddingX` | `int` | `1` | Horizontal padding |
| `PaddingY` | `int` | `0` | Vertical padding |
| `Width` | `int?` | `null` | Fixed width in characters |

---

### `<CliTable>`

```razor
<CliTable Title="Markets"
          Border="@BorderStyle.Rounded"
          Columns="@([ ("Pair", Alignment.Left), ("Price", Alignment.Right) ])"
          Rows="@([ ["BTC/USDT", "$67,420"], ["ETH/USDT", "$3,512"] ])" />
```

---

### `<CliRule>`

```razor
<CliRule Title="Section" RuleStyle="@(new Style { Foreground = Color.DarkCyan })" />
```

---

### `<CliBulletList>` / `<CliOrderedList>`

```razor
<CliBulletList Title="Features" Items="@(["Fast", "Colorful", "Interactive"])" />

<CliOrderedList Title="Steps" Items="@(["Install", "Configure", "Ship"])" />
```

---

### `<CliTreeView>`

```razor
<CliTreeView RootLabel="src" Nodes="@(
[
    new CliTreeNode { Label = "Components", Children =
    [
        new CliTreeNode { Label = "CliPanel.razor" },
        new CliTreeNode { Label = "CliTable.razor" },
    ]},
    new CliTreeNode { Label = "Program.cs" },
])" />
```

---

### `<CliBarChart>`

```razor
<CliBarChart Title="Portfolio" BarWidth="30" Bars="@(
[
    new CliBar { Label = "BTC", Value = 45.2, Color = Color.Orange },
    new CliBar { Label = "ETH", Value = 28.7, Color = Color.Blue },
])" />
```

---

### `<CliProgressBar>`

```razor
<CliProgressBar @ref="_bar" Total="100" Value="0" Label="Loading" Width="40" />

@code {
    CliProgressBar? _bar;

    async Task Run() {
        for (int i = 0; i <= 100; i += 5) {
            await _bar!.SetValue(i);
            await Task.Delay(100);
        }
    }
}
```

---

### `<CliSpinner>`

```razor
<CliSpinner Style="CliSpinner.CliSpinnerStyle.Dots" Label="Processing..." />
```

Available styles: `Dots`, `Line`, `Arrow`, `Clock`, `Bounce`, `Pulse`

---

### `<CliSelectionMenu>`

Arrow keys to navigate, Enter to confirm, Escape to cancel.

```razor
<CliSelectionMenu Prompt="Choose environment:"
                  Options="@([ new CliMenuOption { Label = "Production" },
                                new CliMenuOption { Label = "Staging", CssColor = "#dcdcaa" } ])"
                  OnSelect="@(val => selected = val)" />
```

---

### `<CliMultiSelectMenu>`

Space to toggle, Enter to confirm.

```razor
<CliMultiSelectMenu Prompt="Select features:"
                    Options="@([ new CliMenuOption { Label = "Auth" },
                                  new CliMenuOption { Label = "Logging" } ])"
                    OnConfirm="@(vals => selected = vals)" />
```

---

## Markup

All text content supports the YetAnotherCLISDK markup syntax:

```
[bold red]Error:[/] Something went wrong.
[green]Success:[/] All done.
[#ff6b6b]Custom hex color[/]
[rgb(78,201,176)]RGB color[/]
```

---

## Requirements

- .NET 10+
- Blazor Server
- [YetAnotherCLISDK](https://www.nuget.org/packages/YetAnotherCLISDK) 0.0.2+

---

## License

MIT
