# Using a local engine build during development

When working on ZEngine itself, you don't want to download a release binary — you want Panzerfaust to pick up the engine you just compiled locally. The **Additional engine search path** setting exists for exactly that.

## How it works

Panzerfaust scans two locations for installed engines:

1. **Engine install location** — the managed directory where downloaded releases live (default: `<AppData>/Panzerfaust/AvailablePackages/engines`).
2. **Additional engine search path** — a second directory you supply, typically your local build output. Panzerfaust scans it with the same logic: it looks one level of subdirectories deep for a folder containing an `Obelisk` (or `Obelisk.exe`) binary.

Both locations are scanned every time Panzerfaust starts, and any time you change either path in Settings. Results from both are merged and deduplicated by install path.

## Setting the additional path

1. Open **Settings → Zodiac Engine**.
2. Under **Additional engine search path**, click **…** and select the root folder of your local engine builds, or type the path directly.
3. Panzerfaust immediately rescans and adds any engines it finds to the **Installed** list.
4. The path is saved to `settings.json` and restored on the next launch.

### Example layout

If your local ZEngine build outputs to:

```
~/source/RendererEngine/Result.Darwin.arm64.Debug/
├── bin/
│   └── Obelisk          ← engine binary
└── ...
```

Set the additional path to:

```
~/source/RendererEngine/Result.Darwin.arm64.Debug
```

Panzerfaust will find the `bin/` subdirectory, detect `Obelisk` inside it, and register it as an installed engine named `bin`.

> **Tip:** If you have multiple build configurations (Debug, Release, arm64, x64), point the additional path at their shared parent. Panzerfaust scans one level deep, so each configuration directory is treated as a separate engine version.

### Example with multiple configurations

```
~/source/RendererEngine/
├── Result.Darwin.arm64.Debug/
│   └── bin/Obelisk
├── Result.Darwin.arm64.Release/
│   └── bin/Obelisk
└── Result.Darwin.x64.Debug/
    └── bin/Obelisk
```

Set the additional path to `~/source/RendererEngine` and all three will appear in the **Installed** list as `Result.Darwin.arm64.Debug`, `Result.Darwin.arm64.Release`, and `Result.Darwin.x64.Debug`.

## Pinning a project to a local engine version

Once your local engine appears in the **Installed** list, you can pin any project to it:

1. Open the **Projects** page.
2. Right-click (or use the Info panel) on a project and select **Pin engine version**.
3. Choose the local engine from the list.

The pinned version badge appears on the project card. Panzerfaust will always launch that project with the pinned engine, even if a newer release is available.

## Notes

- The additional path is **not** managed by Panzerfaust — it will never install into or delete from it.
- The path is **only visible in debug builds** via the Settings UI, but it is fully functional in release builds when set programmatically or via `settings.json`.
- To set it in `settings.json` directly, add the key `AdditionalEngineSearchPath` to the file at `<AppData>/Panzerfaust/AppSettings/settings.json`:

```json
{
  "EngineInstallLocation": "/path/to/managed/engines",
  "AdditionalEngineSearchPath": "/path/to/your/local/build",
  "DefaultProjectLocation": "/path/to/projects",
  "SelectedTheme": "Dark"
}
```
