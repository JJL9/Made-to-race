# Tasks

## Purpose

This file tracks current development priorities, task ownership, and active work for Made to Race.

It is intended to help three developers and multiple ChatGPT/Codex sessions avoid duplicated or conflicting work.

Keep this file current as work starts, changes ownership, or is completed.

## Status Labels

Use:

- **Backlog** — not started
- **Ready** — defined and available to claim
- **In Progress** — actively being worked on
- **Blocked** — cannot continue until another dependency or decision is resolved
- **Review** — implementation complete and awaiting review
- **Done** — completed and accepted

## Ownership

Each active task should have one primary owner where practical.

Use:

```text
Owner: Unassigned
Branch: <branch name>
Status: <status>
```

## Current Tasks — M0 Prototype

Goal: **Build a basic car → Drive it → Cross a finish line** (see `MVP.md`).

Order follows the confirmed MVP development order. M0-1 requires the scaffold
PR (`feature/unity-scaffold`) to be merged first; M0-2..M0-7 map to the
requirements in `docs/PRD.md` (§6.1–§6.4).

### M0-1 — Open the Unity project and verify the baseline

Owner: —
Status: Done (verified 2026-08-09)

- Opened the project in Unity **6000.0.81f1** (arm64, headless batchmode);
  generated ProjectSettings and asset `.meta` files are committed.
- Verification: `MadeToRace.Runtime` + `MadeToRace.EditModeTests` compile
  with no errors; EditMode suite **4/4 passed** (BuildValidator tests).
- Note: `-runTests` in batchmode requires running without `-quit` (the
  editor exits before the deferred test run otherwise).

### M0-2 — Basic driving and vehicle movement

Owner: —
Branch: feature/basic-driving
Status: Review (PR pending)

- `VehicleController` v2: throttle/brake/steer + lateral grip.
- `PlayerInputDriver`: WASD/arrows + gamepad → `Drive()`.
- `CameraFollow` + generated prototype scene (`Assets/Scenes/PrototypeDrive.unity`).
- Verified: EditMode 4/4, PlayMode 5/5 (movement physics + keyboard input).

### M0-3 — Basic vehicle construction

Owner: —
Branch: feature/vehicle-builder
Status: Review (PR pending)

- `VehicleBuild` (pure): chassis base + engine/wheel slots, place/remove/
  reset, race-readiness via `BuildValidator`.
- `BuildPhaseController`: visual part attachment + build consequences
  (engine required for power — PRD BLD-1).
- `DebugBuildInput` (prototype): 1 = wheels, 2 = engine, 3 = reset
  (PRD BLD-3). `PrototypeBuild` scene generated headlessly.
- Verified: EditMode 13/13, PlayMode 10/10.

### M0-4 — Vehicle validation

Owner: Unassigned
Branch: feature/vehicle-validation
Status: Ready

- Wire `BuildValidator` into the build flow; invalid builds get clear, fast
  feedback (PRD BLD-5).

### M0-5 — Simple test course

Owner: Unassigned
Branch: feature/test-course
Status: Ready

- Starting area, enough driveable space, finish line (MVP.md; PRD CRS-1).

### M0-6 — Finish detection

Owner: Unassigned
Branch: feature/finish-detection
Status: Ready

- `FinishDetector` + `RaceState` wired: crossing the line reports a completion
  result (PRD RAC-1).

### M0-7 — Reset / retry loop

Owner: Unassigned
Branch: feature/reset-retry
Status: Ready

- Fast reset back to a clean build/drive state (< 5s, PRD RAC-2).

### M0-8 — Minimal build-to-race flow

Owner: Unassigned
Branch:
Status: Backlog

- The full loop runs end-to-end: build → validate → countdown → drive →
  finish → reset.
- M0 success gate: internal playtest answers "is building a vehicle and
  immediately racing it fun?" (MVP.md success criteria).
