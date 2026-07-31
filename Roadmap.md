# Panzerfaust — Roadmap

---

## Engine management

| Status | Feature |
|---|---|
| [x] | Fetch available releases from GitHub |
| [x] | Download and extract engine releases (`.zip` / `.tar.gz`) with progress tracking |
| [x] | Scan installed engines by walking a user-configured directory |
| [x] | Uninstall an engine version |
| [x] | Per-project pinned engine version — persisted in `.pzf`, clearable |
| [x] | Engine picker dialog when opening a project with no pinned version |

---

## Project management

| Status | Feature |
|---|---|
| [x] | Project creation — scaffolds directory structure + `projectConfig.json` |
| [x] | Project list with open / rename / delete |
| [x] | Launches Obelisk with `--projectConfigFile` + `--launchEditor` flags |
| [x] | Cross-platform (Windows / macOS / Linux) |
| [ ] | Project templates — Blank, 3D Starter, 2D Starter, Multiplayer Starter |
| [ ] | Project import — point at existing `projectConfig.json` without recreating structure |
| [ ] | `LastOpenedDate` — track and display when a project was last opened |
| [ ] | `.gitignore` generation at project creation (exclude `CookedAssets/`, `Cache/`, `*.spv`) |

### Project templates

Templates are `.zip` archives shipped with the engine SDK. Panzerfaust extracts the chosen template into the new project directory before writing `projectConfig.json`.

| Template | What it includes |
|---|---|
| Blank | Empty project, no scenes, no scripts |
| 3D Starter | Default scene with floor, directional light, camera. Physics enabled. |
| 2D Starter | Orthographic camera, 2D physics layer preset |
| Multiplayer Starter | Networking configured, rollback template systems pre-registered |

---

## Asset browser

| Status | Feature |
|---|---|
| [x] | Browse Poly Haven 3D model catalog with thumbnail lazy-loading |
| [x] | Remote asset detail panel — format / resolution picker, download with progress |
| [x] | Local asset library — grouped by asset ID, multi-resolution expand |
| [x] | Mesh statistics for `.gltf` files (vertex count, index count, triangle count) |
| [x] | Show in Finder / Explorer / Files |
| [x] | Delete local asset |

---

## `projectConfig.json` schema additions

| Status | Feature |
|---|---|
| [ ] | `buildCommand` field — shell command to build the game DLL |
| [ ] | `gameDllPath` field — path the editor watches for DLL changes (relative to `workingSpace`) |
| [ ] | `schemaVersion` field — for future migration |

```json
{
  "projectName": "MyGame",
  "version": "1.0.0",
  "workingSpace": ".",
  "sceneDir": "Scenes",
  "sceneDataDir": "SceneData",
  "defaultImportDir": {
    "textureDir": "textures",
    "soundDir": "sounds"
  },
  "sceneList": [
    { "name": "Default", "isDefault": true }
  ],
  "buildCommand": "cmake --build Source/build --target ZEngineGame --config Release",
  "gameDllPath": "Source/build/Release/MyGame.dll",
  "schemaVersion": 1
}
```

---

## First-launch setup wizard

| Status | Feature |
|---|---|
| [ ] | Detect installed C++ compiler on first launch |
| [ ] | Save result to `~/.zengine/toolchain.json` |
| [ ] | Auto-generate `buildCommand` in `projectConfig.json` from detected toolchain |
| [ ] | "No compiler found" flow — install link + "Skip for Lua-only" option |
| [ ] | "Reinstall / Change compiler" option in settings |

**Compiler detection per platform:**

| Platform | Method |
|---|---|
| Windows | VSWHERE → find Visual Studio Build Tools / VS 2019/2022 |
| macOS | `xcode-select --print-path` → confirm clang is installed |
| Linux | `which g++` / `which clang++` |

**Generated `buildCommand` per platform:**

| Platform + Compiler | Generated buildCommand |
|---|---|
| Windows + MSVC | `cmake --build Source/build --config Release --target ZEngineGame` |
| Windows + Clang | `cmake --build Source/build --config Release --target ZEngineGame` |
| macOS + Clang | `cmake --build Source/build --config Release --target ZEngineGame` |
| Linux + GCC | `cmake --build Source/build -j$(nproc) --target ZEngineGame` |

---

## Bundled CMake in the SDK

| Status | Feature |
|---|---|
| [ ] | CMake binary shipped inside the SDK package alongside Panzerfaust/Obelisk |
| [ ] | `buildCommand` uses bundled cmake path, not system cmake |

```
ZEngineSDK/
  bin/
    Panzerfaust
    Obelisk
    cmake/
      bin/
        cmake
        cpack
      share/
        cmake-3.x/
  Templates/
    ...
```

---

## Plugin marketplace browser

| Status | Feature |
|---|---|
| [ ] | Browse and install plugins before opening editor |
| [ ] | Calls `StoreClient` REST API |
| [ ] | Downloads `.dll` + `.zplugin` to `<project>/Plugins/` |
| [ ] | Available immediately when editor opens |

---

## Ship wizard

| Status | Feature |
|---|---|
| [ ] | UI — platform selector, config selector, output directory picker |
| [ ] | Step 1: Build game DLL (runs `buildCommand`) |
| [ ] | Step 2: Cook assets (runs `ZCook`) |
| [ ] | Step 3: Package — NSIS/ZIP (Windows), AppImage (Linux), DMG (macOS) |
| [ ] | Step 4 (optional): Upload to Steam via `steamcmd` subprocess |
| [ ] | Auto-generate `app_build.vdf` from `projectConfig.json` |
| [ ] | `CPACK_IGNORE_FILES` enforced — no editor, PluginSDK, source, or raw scripts in package |

**Ship wizard flow:**

```
User clicks [Ship]:

  Target platform:  [Windows]  [Linux]  [macOS]
  Configuration:    [Release]
  Output directory: /path/to/Dist/

  [x] Step 1: Build game DLL (Release)
  [x] Step 2: Cook assets
  [x] Step 3: Package installer
  [ ] Step 4: Upload to Steam  [requires steam credentials]

  [Cancel]                        [Start Ship]
```

Each step must succeed before the next starts. On failure, the exact failing command is shown.

**What the player package contains:**

```
MyGame/
  bin/
    Obelisk          ← engine entry point (no --launchEditor at runtime)
    MyGame.dll       ← compiled game logic
  assets/
    output.pak       ← all cooked content
```

NOT included: Panzerfaust, Tetragrama, PluginSDK, source code, raw Lua scripts, Cache/.
