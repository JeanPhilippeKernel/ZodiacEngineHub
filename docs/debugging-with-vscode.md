# Debugging Panzerfaust with VS Code

## Prerequisites

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0) installed and on your `PATH`
- [VS Code](https://code.visualstudio.com/) with the following extensions (the workspace will prompt you to install them):
  - **C# Dev Kit** (`ms-dotnettools.csdevkit`)
  - **C#** (`ms-dotnettools.csharp`)
  - **Avalonia for VS Code** (`AvaloniaTeam.vscode-avalonia`)

---

## Starting a debug session

1. Open the repository root in VS Code.
2. Press `F5` (or go to **Run → Start Debugging**).

VS Code will build the project in `Debug` configuration, launch `Panzerfaust`, and stop at any breakpoint you have set. The first build takes a few seconds; subsequent launches reuse the incremental build.

---

## Setting breakpoints

Click the gutter to the left of any line number in a `.cs` file. A red dot confirms the breakpoint is active.

Useful places to start:

| What you want to inspect | Where to break |
|---|---|
| App startup / DI wiring | `App.axaml.cs` — `OnFrameworkInitializationCompleted` |
| Page navigation | `MainWindowViewModel.cs` — `NavigateCommand` handler |
| Project load | `MainWindowViewModel.cs` — `LoadProjects` |
| Engine release fetch | `MainWindowViewModel.cs` — `FetchReleasesAsync` |
| Download progress | `MainWindowViewModel.cs` — `TrackDownload` |
| Asset detail open | `MainWindowViewModel.cs` — `OnShowAssetDetail` |

---

## Inspecting values at a breakpoint

When execution pauses:

- **Variables panel** (left sidebar) — locals, parameters, and `this`
- **Watch panel** — any C# expression, e.g. `_projects.Count`, `SelectedRelease?.DisplayName`
- **Debug Console** (`Ctrl+Shift+Y` / `⇧⌘Y` on macOS) — evaluate expressions interactively
- **Call Stack panel** — click any frame to jump to that call site

Hover over a variable in the editor to see its current value inline.

---

## Conditional and logpoint breakpoints

Right-click a breakpoint dot to edit it:

- **Condition** — only pause when an expression is true, e.g. `ProjectCount > 0`
- **Hit Count** — pause after the line has been hit N times
- **Log Message** — print to the Debug Console without stopping; use `{expression}` to embed values, e.g. `Download progress: {op.Progress}%`

---

## Available tasks

Open the task runner with `Ctrl+Shift+B` (Windows / Linux) or `⇧⌘B` (macOS), or via `Ctrl+Shift+P` / `⇧⌘P` → **Tasks: Run Task**.

| Task | What it does |
|---|---|
| `build-debug` *(default)* | Debug build for the host platform |
| `publish-release` | Self-contained Release publish — picks the correct RID per OS automatically |
| `clean` | Delete `bin/` and `obj/` |
| `watch` | `dotnet watch run` — rebuilds and relaunches on file save |

---

## Attaching to a running instance

If the app is already running (e.g. launched from a terminal):

1. Select the **Attach to process** configuration from the debug dropdown.
2. Press `F5` — a process picker appears.
3. Search for `Panzerfaust` and select it.

---

## Avalonia previewer

Open any `.axaml` file and click **Preview** in the editor title bar (or `Ctrl+Shift+P` / `⇧⌘P` → **Avalonia: Show Preview**).

If the previewer fails to start with a "missing framework" error, the Avalonia extension cannot locate your .NET 10 installation. Add the following to your **personal** VS Code user settings (`Ctrl+Shift+P` → **Open User Settings (JSON)**) — do not add it to the workspace `settings.json`, as the path differs per machine:

| Platform | Setting |
|---|---|
| **Windows** | `"avalonia.dotnetPath": "C:\\Program Files\\dotnet\\dotnet.exe"` |
| **macOS (Homebrew)** | `"avalonia.dotnetPath": "/opt/homebrew/bin/dotnet"` |
| **Linux** | `"avalonia.dotnetPath": "/usr/bin/dotnet"` |

Adjust the path to match where your SDK is actually installed (`where dotnet` on Windows, `which dotnet` on macOS/Linux).

---

## Troubleshooting

**Breakpoints show as hollow circles ("unverified")**
Symbols load after the runtime initialises — wait until the app window appears. If they stay unverified, run **clean** then rebuild.

**`dotnet` not found**
Run `dotnet --version` in a terminal. It should print `10.x.x`. If not, follow the [.NET 10 install guide](https://dotnet.microsoft.com/download/dotnet/10.0) for your platform.

**Build fails on first launch**
Run the hook installer once after cloning (see [Contributing.md](../Contributing.md#local-setup)), then try again.
