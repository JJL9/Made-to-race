# Unity Setup

How to turn this repository into a working Unity project (one-time setup).

## Prerequisites

- [Unity Hub](https://unity.com/download) installed.
- Unity **6 LTS** editor installed via Hub (any current 6.x LTS). The project
  records a specific patch in `ProjectSettings/ProjectVersion.txt`; if your
  installed version differs, accept Hub's version prompt — Unity upgrades the
  project on first open.
- **Git LFS** (one-time, per machine):
  - macOS: `brew install git-lfs && git lfs install`
  - Windows: https://git-lfs.com (or bundled with Git for Windows)
  - Linux: `sudo apt install git-lfs && git lfs install`

## Open the project

1. `git clone git@github.com:JJL9/Made-to-race.git` (or pull the latest `main`).
2. Unity Hub → **Add** → **Add project from disk** → select the repo root.
3. If Hub/Unity prompts about the editor version, open with the installed
   Unity 6 LTS (expected — accept).
4. First open generates the remaining `ProjectSettings/`, `Library/`, and
   default `Packages/` entries. `Library/` is gitignored — never commit it.

## Verify

- Project opens without console errors.
- Window → General → **Test Runner** → EditMode: run the
  `MadeToRace.EditModeTests` suite — all tests pass.
- The `MadeToRace.Runtime` assembly compiles (see `Assets/Scripts/`).

## Windows build support (for Steam)

- In Unity Hub, add **Windows Build Support (Mono)** to the Unity 6 LTS
  installation so this Mac can produce Windows builds later.

## Git LFS usage

- Large binaries (art, audio, models, video) are tracked via Git LFS
  (see `.gitattributes`). After adding such a file, verify with
  `git lfs ls-files`.
- Unity text assets (`.prefab`, `.unity`, `.asset`) are YAML text and are
  committed normally — never force them into LFS.
