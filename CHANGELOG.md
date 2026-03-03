# Code Editor Package for Antigravity — Changelog

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
