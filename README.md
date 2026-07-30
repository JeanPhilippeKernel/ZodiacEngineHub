# Panzerfaust

[![CI](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/actions/workflows/Engine-CI.yml/badge.svg)](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/actions/workflows/Engine-CI.yml)
[![Release](https://img.shields.io/github/v/release/JeanPhilippeKernel/ZodiacEngineHub)](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/releases/latest)
[![License](https://img.shields.io/github/license/JeanPhilippeKernel/ZodiacEngineHub)](LICENSE)

Panzerfaust is the official launcher and project manager for [ZEngine](https://github.com/JeanPhilippeKernel/RendererEngine). It lets game developers install and manage ZEngine versions, create and open projects, and browse local assets — all from a single desktop application.

| Projects | Assets store | Zodiac Engine |
|---|---|---|
| ![Projects page](docs/Screenshot%202026-07-30%20at%2021.09.45.png) | ![Assets store](docs/Screenshot%202026-07-30%20at%2021.10.50.png) | ![Zodiac Engine](docs/Screenshot%202026-07-30%20at%2021.11.18.png) |

---

## Features

- **Engine management** — download, install, and switch between ZEngine releases
- **Project management** — create new projects, open existing ones, and launch them directly into the ZEngine editor
- **Local asset browser** — browse and manage assets grouped by asset ID, with per-version detail and mesh statistics
- **Cross-platform** — runs natively on Windows, macOS (Intel & Apple Silicon), and Linux

---

## Download

Pre-built binaries are available on the [Releases](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/releases) page for all supported platforms.

| Platform | Architecture | Download |
|---|---|---|
| Windows | x64 | `Panzerfaust-*-Windows-x64.zip` |
| macOS | x64 (Intel) | `Panzerfaust-*-macOS-x64.zip` |
| macOS | arm64 (Apple Silicon) | `Panzerfaust-*-macOS-arm64.zip` |
| Linux | x64 | `Panzerfaust-*-Linux-x64.tar.gz` |

---

## System requirements

| Platform | Minimum OS |
|---|---|
| Windows | Windows 10 (64-bit) |
| macOS | macOS 12 Monterey |
| Linux | Ubuntu 22.04 or equivalent |

---

## Building from source

**Prerequisites**

- [.NET SDK 10](https://dotnet.microsoft.com/download/dotnet/10.0)

**Clone and run**

```sh
git clone https://github.com/JeanPhilippeKernel/ZodiacEngineHub.git
cd ZodiacEngineHub
sh Scripts/install-hooks.sh
dotnet run --project Panzerfaust.csproj
```

**Publish a self-contained binary**

```sh
# Windows
dotnet publish Panzerfaust.csproj --configuration Release --runtime win-x64 --self-contained true

# macOS (Apple Silicon)
dotnet publish Panzerfaust.csproj --configuration Release --runtime osx-arm64 --self-contained true

# Linux
dotnet publish Panzerfaust.csproj --configuration Release --runtime linux-x64 --self-contained true
```

---

## Contributing

See [Contributing.md](Contributing.md) for commit conventions, branch rules, and the release process. All pull requests must target the `develop` branch.

For local development with a custom engine build, see [docs/local-engine-path.md](docs/local-engine-path.md).

---

## License

Panzerfaust is licensed under the [MIT License](LICENSE).
