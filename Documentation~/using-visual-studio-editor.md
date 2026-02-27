# Using the Antigravity Editor Package

## Set Antigravity as External Script Editor

1. Open **Unity > Preferences > External Tools** (macOS) or **Edit > Preferences > External Tools** (Windows)
2. In the **External Script Editor** dropdown, select **Antigravity**
3. The panel will reload and show additional settings

## Generate .csproj Files

The package generates `.csproj` and `.sln` files so Antigravity has full C# IntelliSense and Unity API awareness. Use the checkboxes to control which package types get a `.csproj` file:

| Setting               | Description                                                          |
| --------------------- | -------------------------------------------------------------------- |
| **Embedded packages** | Packages inside your project's `Packages/` folder                    |
| **Local packages**    | Packages installed from a local path outside the project             |
| **Registry packages** | Packages from Unity or a custom package registry                     |
| **Git packages**      | Packages installed via a Git URL                                     |
| **Built-in packages** | Packages bundled with the Unity installation                         |
| **Tarball packages**  | Packages installed from a local `.tgz` archive                       |
| **Unknown packages**  | Packages with an unrecognized or missing origin                      |
| **Player projects**   | Generates an extra `ProjectName.Player.csproj` for player assemblies |

Click **Regenerate project files** to apply changes.

## Workspace Config Files

When opening a project, the package automatically creates or patches these files inside `.vscode/` in your Unity project root (Antigravity reads these as a VS Code-based editor):

| File                      | Purpose                                                                                     |
| ------------------------- | ------------------------------------------------------------------------------------------- |
| `.vscode/extensions.json` | Recommends the `visualstudiotoolsforunity.vstuc` Unity extension                            |
| `.vscode/settings.json`   | Excludes Unity binary/generated files from the file explorer; sets `dotnet.defaultSolution` |
| `.vscode/launch.json`     | Adds an "Attach to Unity" debug configuration                                               |

To prevent the package from patching an existing file, create a `.vstupatchdisable` file inside `.vscode/`.

## Reuse Existing Window

When Antigravity is the active editor, a **"Reuse existing Antigravity window"** toggle appears in Preferences. When enabled, double-clicking a script opens it in the already-running Antigravity instance that has the project open, instead of launching a new window.
