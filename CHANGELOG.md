# Code Editor Package for Antigravity — Changelog

## [1.0.6] - 2026-05-23

### Fixed

- Unity no longer routes C# script opens to the standalone **Antigravity 2.0 agent app**. Google's I/O 2026 release split the product line into the agent-first "Antigravity" desktop app and the VS Code-fork "Antigravity IDE"; previously the package discovered both, so users with both installed could end up launching the agent app (which has no editor surface) on every script open.
  - `IsCandidateForDiscovery` now requires `Antigravity IDE*.exe` / `Antigravity IDE*.app` / `antigravity-ide[-insiders]` filenames.
  - `TryDiscoverInstallation` reads `resources/app/package.json` and rejects any install whose product name is not `Antigravity IDE` (defence-in-depth if a user points External Tools at the agent app manually).
  - `GetVisualStudioInstallations` only lists IDE candidates; the plain `Antigravity.exe` / `antigravity` / `antigravity.desktop` paths were removed on Windows, macOS and Linux.
  - Reuse-existing-window process matching no longer matches the agent app's `Antigravity` / `antigravity` process names.
  - Workspace storage scan now reads `Antigravity IDE/User/workspaceStorage` (the IDE's new per-product AppData / `Application Support` / `.config` location) instead of the legacy `Antigravity/...` path that now belongs to the agent app.
  - External script editor entries are always labelled `Antigravity IDE` — the ambiguous bare `Antigravity` display name was removed.

### Changed

- Insider builds (`Antigravity IDE - Insider.exe`, `antigravity-ide-insiders`, `Antigravity IDE - Insider*.app`) are now recognised on all three platforms.
- Package display name updated to **Antigravity IDE Editor**; package description refreshed to mention the Antigravity 2.0 split explicitly.
- Documentation (`README.md`, `Documentation~/index.md`, `Documentation~/using-visual-studio-editor.md`) updated to reflect the IDE rename, new install paths, and product-name detection.

### License

- **License changed from MIT to a dual license** starting with this release:
  - Inherited upstream code originating from `com.unity.ide.vscode` (© Unity Technologies, © Microsoft Corporation) remains under the **MIT License**.
  - All new contributions to this fork are released under the **[PolyForm Noncommercial License 1.0.0](https://polyformproject.org/licenses/noncommercial/1.0.0)**.
  - **Commercial use requires a separate commercial license** from the copyright holder. Open a GitHub issue at https://github.com/BadranRaza/com.unity.ide.antigravity/issues to start the conversation.
  - Releases up to and including **v1.0.5 remain available under the original MIT License**; the new terms apply to v1.0.6 and later.

## [1.0.5] - 2026-03-09

### Fixed

- Removed orphaned `Editor/Plugins.meta` so Unity no longer warns about a missing `Packages/com.unity.ide.antigravity/Editor/Plugins` folder during import

## [1.0.4] - 2026-03-03

### Fixed

- Removed `AppleEventIntegration.bundle` from this package to avoid duplicate editor plugin-name conflicts when `com.boxqkrtm.ide.cursor` is installed

## [1.0.3] - 2026-03-03

### Fixed

- Disabled `AppleEventIntegration.bundle` for this package to avoid duplicate editor plugin-name conflicts when `com.boxqkrtm.ide.cursor` is installed

## [1.0.2] - 2026-03-03

### Fixed

- Regenerated all package `.meta` GUIDs to prevent GUID conflicts with `com.boxqkrtm.ide.cursor` when both packages are installed in the same project

## [1.0.0] - 2026-02-27

### Changed

- Workspace config files (`launch.json`, `settings.json`, `extensions.json`) now written to `.vscode/` — Antigravity reads these as a VS Code fork
- Extensions are looked up from `~/.antigravity/extensions/` (Antigravity's native extension directory)
- Fixed uninitialized `antigravityStoragePath` variable on unsupported platforms (added `#else` early return)
- Removed no-op `IOPath.Combine` wrappers on `GetFolderPath` calls (macOS and Windows)
- Removed orphaned `.meta` files for deleted Cursor and Codium installation classes

## [1.0.0-beta] - 2026-02-25

### Added

- Initial release: Google Antigravity integration for Unity as an external code editor
- Auto-discovery of Antigravity installations on Windows, macOS, and Linux
- Generates `.sln` and `.csproj` files for IntelliSense support
- "Reuse existing Antigravity window" preference toggle
- Workspace detection via Antigravity's `workspaceStorage` directory
