# Security policy

## Supported versions

This is a small Unity editor package. The latest tagged release is the only
version that receives fixes. If you're on an older version, please update
first and check whether the issue still reproduces.

| Version | Status |
|---|---|
| v1.0.7+ | Supported |
| ≤ v1.0.6 | Not supported — please upgrade |

## Reporting a vulnerability

If you've found something that looks like a security issue (anything that
could let untrusted input run arbitrary code through the package, leak
files outside the Unity project, write to paths it shouldn't, etc.), please
**don't** open a public GitHub issue.

Instead, use GitHub's private vulnerability reporting:

→ https://github.com/BadranRaza/com.unity.ide.antigravity/security/advisories/new

This is a hobby project maintained in spare time, so response isn't
guaranteed within any fixed window. I'll do my best to acknowledge within a
week and have a fix or mitigation out within a month for anything
exploitable. If the issue is sensitive and you'd prefer to coordinate
disclosure timing, just say so in the advisory.

## Scope

In scope:

- The C# code under `Editor/` in this repo
- Any process this package launches on behalf of the user (i.e. how it
  invokes Antigravity IDE)
- The discovery logic that walks filesystem paths to find Antigravity IDE
  installs

Out of scope:

- Bugs in Antigravity IDE itself — report those to Google's Antigravity
  forum at https://discuss.ai.google.dev/c/google-antigravity
- Bugs in Unity itself or in `com.unity.ide.vscode` upstream
- Anything in the upstream MIT-licensed code that hasn't been modified here
  (those should go to the relevant upstream maintainer)
