# Code Editor Package for Google Antigravity IDE

This package integrates **Google Antigravity IDE** as the external code editor
for Unity. Antigravity IDE is the VS Code-based code editor in the Antigravity
product line. It is **not** the same product as the standalone "Antigravity"
desktop app (the agent-orchestration tool launched as part of Antigravity 2.0
at Google I/O 2026) — see [Antigravity 2.0 split](#antigravity-20-split)
below.

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

## License

This package is **dual-licensed** starting with v1.0.6:

- **Inherited upstream code** from `com.unity.ide.vscode`
  (© Unity Technologies, © Microsoft Corporation) remains under the **MIT
  License**.
- **All new contributions** to this fork (the Antigravity IDE integration
  authored by BadranRaza and contributors) are released under the
  **[PolyForm Noncommercial License 1.0.0](https://polyformproject.org/licenses/noncommercial/1.0.0)**.

In plain English: free for personal projects, education, research, charity
and government use. **Commercial use** — including paid products, paid
services, commercial Asset Store listings, contracted client work or internal
tooling at a for-profit company — **requires a separate commercial license**
from the copyright holder.

Releases up to and including **v1.0.5** remain available under the original
MIT License. The PolyForm Noncommercial terms apply only to **v1.0.6 and
later** releases.

### Commercial Licensing

To request a commercial license (flat fee, per-seat or revenue-share terms
are negotiable), open a GitHub issue describing your intended use:

- https://github.com/BadranRaza/com.unity.ide.antigravity/issues

A dedicated contact email will be published in a future release.

See [`LICENSE.md`](LICENSE.md) for the complete legal text.
