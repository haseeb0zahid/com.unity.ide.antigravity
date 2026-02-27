# Code Editor Package for Google Antigravity

This package integrates **Google Antigravity** as the external code editor for Unity.

## Features

- **Auto-Discovery**: Automatically finds your Antigravity installation on macOS, Windows, and Linux.
- **IntelliSense**: Generates `.sln` and `.csproj` files to provide complete C# IntelliSense and Unity API support inside Antigravity.
- **Workspace Setup**: Automatically creates `.vscode/` workspace config files (`launch.json`, `settings.json`, `extensions.json`) that Antigravity reads as a VS Code-based editor.
- **Reuse Window**: Optional setting to open files in an already-running Antigravity window instead of launching a new one.

## Installation

Install via Unity Package Manager using the git URL:

1. Open `Window > Package Manager`
2. Click `+` → **Add package from git URL...**
3. Enter:
   ```
   https://github.com/BadranRaza/com.unity.ide.antigravity.git
   ```
4. Click **Add**

The package will appear in Package Manager and can be updated from there when new versions are released.

> **Alternative:** If you cloned the repo locally, use `+ → Add package from disk...` and point to the `package.json`.

## Configuration

1. Go to `Unity > Preferences > External Tools` (macOS) or `Edit > Preferences > External Tools` (Windows)
2. Select **Antigravity** from the **External Script Editor** dropdown
3. Choose which package types should have `.csproj` files generated
4. Click **Regenerate project files** to apply

### Reuse Existing Window

When Antigravity is selected as the editor, a **"Reuse existing Antigravity window"** toggle appears in Preferences. When enabled, double-clicking a script in Unity will open it in an already-running Antigravity instance instead of launching a new one.

## How It Works

- **Workspace config** is written to `.vscode/` in your Unity project root — Antigravity reads these as a VS Code fork.
- **Extensions** are looked up from `~/.antigravity/extensions/` — Antigravity's native user-level extension directory.
- **Window reuse** works by reading Antigravity's `workspaceStorage` to find which workspace a running instance has open.

## Requirements

- Unity 2019.4 or later
- Antigravity IDE installed ([antigravity.google](https://antigravity.google))
