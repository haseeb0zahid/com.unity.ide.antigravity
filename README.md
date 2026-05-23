# Code Editor Package for Google Antigravity IDE

[![Latest release](https://img.shields.io/github/v/release/BadranRaza/com.unity.ide.antigravity?label=release&sort=semver&color=blue)](https://github.com/BadranRaza/com.unity.ide.antigravity/releases/latest)
[![Unity 2019.4+](https://img.shields.io/badge/Unity-2019.4%2B-black?logo=unity)](https://unity.com/)
[![License: MIT](https://img.shields.io/github/license/BadranRaza/com.unity.ide.antigravity?color=brightgreen)](LICENSE.md)
[![GitHub stars](https://img.shields.io/github/stars/BadranRaza/com.unity.ide.antigravity?style=social)](https://github.com/BadranRaza/com.unity.ide.antigravity/stargazers)
[![GitHub forks](https://img.shields.io/github/forks/BadranRaza/com.unity.ide.antigravity?style=social)](https://github.com/BadranRaza/com.unity.ide.antigravity/network/members)

Integrates **Google Antigravity IDE** as the external code editor for Unity.
Antigravity IDE is the VS Code-based code editor in the Antigravity product
line. It is **not** the same product as the standalone "Antigravity" desktop
app (the agent-orchestration tool launched as part of Antigravity 2.0 at
Google I/O 2026) — see [Antigravity 2.0 split](#antigravity-20-split) below.

If this package saves you time, please ⭐ star the repo — it is the single
biggest signal that helps other Unity developers find it.

## Features

- **Auto-Discovery**: automatically finds your **Antigravity IDE**
  installation on macOS, Windows and Linux (and ignores the standalone
  Antigravity agent app, which cannot host script editing).
- **IntelliSense**: generates `.sln` and `.csproj` files for complete C#
  IntelliSense and Unity API support inside Antigravity IDE.
- **Workspace Setup**: writes `.vscode/` workspace config files
  (`launch.json`, `settings.json`, `extensions.json`) that Antigravity IDE
  reads as a VS Code fork.
- **Reuse Window**: optional setting to open files in an already-running
  Antigravity IDE window instead of launching a new one. Backed by a scan of
  Antigravity IDE's own `workspaceStorage` directory.
- **Insider builds supported**: `Antigravity IDE - Insider` /
  `antigravity-ide-insiders` are discovered on all three platforms.

## Installation

Install via Unity Package Manager using the git URL:

1. Open `Window > Package Manager`
2. Click `+` → **Add package from git URL...**
3. Enter:
   ```
   https://github.com/BadranRaza/com.unity.ide.antigravity.git
   ```
4. Click **Add**

The package will appear in Package Manager and can be updated from there when
new versions are released.

> **Alternative**: if you cloned the repo locally, use
> `+ → Add package from disk...` and point to the `package.json`.

## Configuration

1. Install **Antigravity IDE** from
   [antigravity.google](https://antigravity.google).
2. Go to `Unity > Preferences > External Tools` (macOS) or
   `Edit > Preferences > External Tools` (Windows / Linux).
3. Select **Antigravity IDE** from the **External Script Editor** dropdown.
   If you previously had a plain "Antigravity" entry selected, that was the
   agent app — pick "Antigravity IDE" instead.
4. Choose which package types should have `.csproj` files generated.
5. Click **Regenerate project files** to apply.

### Reuse Existing Window

When Antigravity IDE is selected as the editor, a
**"Reuse existing Antigravity window"** toggle appears in Preferences. When
enabled, double-clicking a script in Unity will open it in an already-running
Antigravity IDE instance instead of launching a new one.

## Antigravity 2.0 split

At Google I/O 2026 (May 19, 2026) Google split the Antigravity product line
into two separate desktop applications:

| Product | What it is | Default install dir (Windows) | Process / exe name |
|---|---|---|---|
| **Antigravity** (2.0) | Agent-first standalone desktop app. Multi-agent orchestration UI. **Not a code editor.** | `…\Programs\Antigravity\Antigravity.exe` | `Antigravity.exe` |
| **Antigravity IDE** | VS Code fork. The actual code editor. **This is what Unity needs.** | `…\Programs\Antigravity IDE\Antigravity IDE.exe` | `Antigravity IDE.exe` |

Both share the same Code-fork shell. They are told apart by
`resources/app/package.json` `name` field (`"Antigravity"` vs
`"Antigravity IDE"`), which this package verifies during discovery. Older
versions of this package (≤ 1.0.5) matched any `Antigravity*.exe`, which on
machines with both products installed could route Unity script opens into the
agent app. Starting in **v1.0.6**, only Antigravity IDE installs are listed.

## How It Works

- **Workspace config** is written to `.vscode/` in your Unity project root —
  Antigravity IDE reads these as a VS Code fork.
- **Extensions** are looked up from `~/.antigravity-ide/extensions/`
  (`~/.antigravity-ide-insiders/extensions/` for insider builds) — Antigravity
  IDE's user-level extension directory.
- **Window reuse** works by reading Antigravity IDE's `workspaceStorage`
  directory to find which workspace a running instance has open:
  - Windows: `%APPDATA%\Antigravity IDE\User\workspaceStorage`
  - macOS: `~/Library/Application Support/Antigravity IDE/User/workspaceStorage`
  - Linux: `~/.config/Antigravity IDE/User/workspaceStorage`

## Requirements

- Unity 2019.4 or later
- **Antigravity IDE** installed from
  [antigravity.google](https://antigravity.google) (the standalone
  Antigravity 2.0 agent app does not work as a Unity script editor)

## Support the project

If this package made your Unity + Antigravity IDE setup work:

- ⭐ **Star the repository** — biggest single thing you can do to help
  others find it.
- 💬 Open an issue or discussion if you hit a bug or have a feature request.
- 💖 [Sponsor on GitHub](https://github.com/sponsors/BadranRaza) to support
  continued maintenance.

## License

[MIT](LICENSE.md). Free to use in personal, commercial and Asset Store
projects. Includes upstream MIT-licensed code from Unity Technologies and
Microsoft Corporation (the `com.unity.ide.vscode` package this is forked
from).
