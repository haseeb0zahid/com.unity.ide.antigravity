# Why Unity started opening the Antigravity agent app instead of the IDE (and the fix)

> tl;dr — Google split the Antigravity product at I/O 2026. The standalone
> "Antigravity" desktop app is now an agent-orchestration tool, not a code
> editor. The actual code editor is a separate install called "Antigravity
> IDE". Most Unity packages still match any `Antigravity*.exe`, so they hand
> Unity the wrong app. This page explains what changed and how the package
> in this repo detects the right one. If you just want the fix, install
> v1.0.7+ and re-select **Antigravity IDE** in `Edit > Preferences > External
> Tools`.

## What actually changed on May 19, 2026

At Google I/O 2026 Google announced **Antigravity 2.0**, an "agent-first"
rework of the Antigravity product line. It shipped as a separate desktop
binary alongside the existing IDE, and they renamed the IDE to disambiguate.

So now there are two installs:

| Product | What it actually is | Windows install dir | Manifest `name` |
|---|---|---|---|
| **Antigravity** (2.0) | Multi-agent orchestration UI in a Chromium shell. No code-editor surface — opening a `.cs` file in it just launches the agent home screen. | `%LOCALAPPDATA%\Programs\Antigravity\Antigravity.exe` | `"Antigravity"` |
| **Antigravity IDE** | VS Code fork. The thing that actually has a text editor, tabs, IntelliSense, etc. | `%LOCALAPPDATA%\Programs\Antigravity IDE\Antigravity IDE.exe` | `"Antigravity IDE"` |

Both apps share the same Code-fork shell and have an identical
`resources/app/` layout, including the same `distro` hash. The only reliable
runtime signal that tells them apart is the `name` field in
`resources/app/package.json`.

AppData paths split the same way:

- Windows: `%APPDATA%\Antigravity` (agent app) vs `%APPDATA%\Antigravity IDE` (IDE)
- macOS: `~/Library/Application Support/Antigravity` vs `~/Library/Application Support/Antigravity IDE`
- Linux: `~/.config/Antigravity` vs `~/.config/Antigravity IDE`

Extensions also moved:

- Old: `~/.antigravity/extensions`
- New: `~/.antigravity-ide/extensions` (or `~/.antigravity-ide-insiders/extensions`)

## Why this silently broke Unity

Unity's "external script editor" support is implemented per-IDE through
small packages that derive (originally) from `com.unity.ide.vscode`. Every
community Antigravity package I've looked at — and the one in this repo,
prior to v1.0.6 — used a single regex during discovery:

```
.*Antigravity.*.exe$        (Windows)
.*Antigravity.*\.app$        (macOS)
ends-with antigravity         (Linux)
```

That regex was perfectly fine when there was only one Antigravity desktop
binary. After May 19, it happily matches both the IDE and the agent app.
If you ended up with both installed (and a lot of people did, since the
2.0 installer doesn't remove the IDE), Unity's External Tools dropdown
listed *two* "Antigravity" entries. Either Unity auto-picked the first
candidate or the user picked the wrong one. From then on every
double-clicked script launched the agent app's home screen and developers
sat there wondering why their editor "broke".

It's worse than that, actually: even if you manually `Browse...` to the
correct `Antigravity IDE.exe`, the old discovery code labelled it just
"Antigravity" in the dropdown, indistinguishable from the agent-app entry.

## The fix in this package (v1.0.6+)

Three layers:

### 1. Filename-level filtering

`IsCandidateForDiscovery` now requires the IDE-specific filename:

```csharp
#if UNITY_EDITOR_WIN
    return File.Exists(path) && Regex.IsMatch(path, ".*Antigravity IDE.*\\.exe$", RegexOptions.IgnoreCase);
#elif UNITY_EDITOR_OSX
    return Directory.Exists(path) && Regex.IsMatch(path, ".*Antigravity IDE.*\\.app$", RegexOptions.IgnoreCase);
#else
    return File.Exists(path) && (path.EndsWith("antigravity-ide", StringComparison.OrdinalIgnoreCase)
                              || path.EndsWith("antigravity-ide-insiders", StringComparison.OrdinalIgnoreCase));
#endif
```

The wildcard `.*Antigravity IDE.*\.exe$` (and equivalents on the other
platforms) is broad enough to also catch `Antigravity IDE - Insider.exe`
and similar future variants without picking up the bare `Antigravity.exe`.

### 2. Manifest-level verification

`TryDiscoverInstallation` reads `resources/app/package.json` and rejects
anything whose `name` field isn't `Antigravity IDE`. This is the
defence-in-depth — if a user has weird symlinks, or manually points
External Tools at `Antigravity.exe` via Browse, the candidate is still
rejected because the manifest says it's the agent app:

```csharp
if (!string.IsNullOrEmpty(manifestName) &&
    manifestName.IndexOf("Antigravity IDE", StringComparison.OrdinalIgnoreCase) < 0)
    return false;
```

### 3. Search-path narrowing

`GetVisualStudioInstallations` only looks at known IDE locations. The plain
`Antigravity.exe` / `antigravity` / `antigravity.desktop` paths were
removed from Windows, macOS and Linux candidate lists. So even if both
products are installed, only the IDE shows up in the dropdown — there's no
ambiguous "Antigravity" entry any more.

A few other places also needed adjusting:

- **Reuse-window process matching** (`Process.GetProcessesByName`) — used
  to also include plain "Antigravity" / "antigravity"; those are the agent
  app's process names now, so they're gone.
- **Workspace storage scan** (`ProcessRunner.GetProcessWorkspaces`) — used
  to read `\Roaming\Antigravity\User\workspaceStorage`; now reads
  `\Roaming\Antigravity IDE\User\workspaceStorage` (and the macOS / Linux
  equivalents).
- **Display name** — always `Antigravity IDE` in the External Tools
  dropdown, never the ambiguous bare `Antigravity`.

## Migrating from an older version

If you were on v1.0.5 or earlier:

1. Update the package via Unity Package Manager (the git URL hasn't
   changed).
2. Open `Edit > Preferences > External Tools`. If your saved selection was
   the agent app's executable, the discovery code will refuse to register
   it next domain reload — re-pick **Antigravity IDE** from the dropdown.
3. Domain reload by closing and reopening Unity, or by toggling Play mode.
4. Double-click any C# script. It should launch `Antigravity IDE.exe`, not
   `Antigravity.exe`. The `Antigravity IDE` window title is the easiest way
   to confirm.

If you previously had Antigravity workspace state in `\Roaming\Antigravity`
that you still want for the reuse-window feature, copy the relevant
`workspaceStorage` subfolder across to `\Roaming\Antigravity IDE`. Easier
in practice is to just open the Unity project in Antigravity IDE once,
which writes a fresh entry.

## What this doesn't fix

- **Microsoft's official C# / C# Dev Kit / Unity extensions** are licensed
  for VS Code only and won't install on Antigravity IDE (or any other VS
  Code fork). Use **DotRush** instead — open-source Roslyn-based C# server,
  available from the Extensions panel inside Antigravity IDE.
- **The standalone Antigravity 2.0 agent app** can't be turned into a code
  editor by this package. It's a separate product with its own scope. If
  you want the agent UI for orchestration *and* the IDE for writing code,
  install both side by side; this package just makes sure Unity routes
  script opens to the right one.

## References

- TechCrunch, *"Google launches Antigravity 2.0 with an updated desktop app and CLI tool at I/O 2026"* — https://techcrunch.com/2026/05/19/google-launches-antigravity-2-0-with-an-updated-desktop-app-and-cli-tool/
- MarkTechPost, *"Google Launches Antigravity 2.0 at I/O 2026: A Standalone Agent-First Platform"* — https://www.marktechpost.com/2026/05/19/google-launches-antigravity-2-0-at-i-o-2026-a-standalone-agent-first-platform-with-cli-sdk-managed-execution-and-enterprise-support/
- Piunika, *"Google Antigravity 2.0 broken? Missing IDE and folder fixes explained"* — https://piunikaweb.com/2026/05/20/fix-google-antigravity-2-0-missing-ide-error/
- InfoWorld, *"Google to unify AI coding tools under Antigravity"* — https://www.infoworld.com/article/4175416/google-to-unify-ai-coding-tools-under-antigravity.html
