# Architecture

## Purpose

This document records the technical architecture and major system boundaries for Made to Race.

It should describe accepted implementation structure once those decisions exist.

Do not use this file to speculate about architecture that has not been selected.

## Current Status

**Confirmed — 2026-08-09**

- **Engine:** Unity 6 LTS (exact 6.x patch pinned at project creation)
- **Language:** C# (IL2CPP for release builds)
- **Distribution:** Steam (Windows first; Steam Deck support as a goal)
- **Render pipeline:** URP
- **Physics:** Unity built-in (PhysX); engineer-grade simcade vehicle model (see Vehicle Physics section)
- **Version control:** Git + Git LFS for large binaries

The Unity-specific structure below is a starting point and should be revisited after the first playable prototype.

## Architectural Principles

**Confirmed**

The project should prefer:

- small, testable systems
- readable and modular code
- clear responsibility boundaries
- minimal dependencies
- simple implementations before complex abstractions
- gameplay logic separated from presentation where practical
- physics/simulation separated from visuals where practical
- UI separated from gameplay rules where practical
- networking concerns separated from purely local presentation where practical

These principles are guidelines, not requirements for unnecessary abstraction.

## Multiplayer Direction

**Confirmed**

Made to Race is multiplayer-first in the long term.

Early local systems should avoid obvious architectural choices that would make multiplayer unnecessarily difficult later.

However, the project should not build complex networking infrastructure before the local core gameplay is proven.

The immediate playable goal remains:

**Build a basic car → Drive it → Cross a finish line**

## Initial System Areas

The following areas are expected to exist in some form, but their exact architecture is not yet finalized:

- vehicle construction
- vehicle physics and movement
- player input
- race state
- checkpoints and finish detection
- UI
- visual presentation
- networking
- persistent player data

These are conceptual responsibilities, not finalized classes, folders, modules, or engine objects.

## Architecture Decision Process

When a major architectural decision becomes accepted:

1. Confirm the decision with the team.
2. Record the decision in `docs/DECISIONS.md`.
3. Update this document with the resulting structure.
4. Update `CODEX.md` if AI coding instructions are affected.
5. Update other documentation if system boundaries or workflows change.

Do not silently establish major architecture through implementation alone.

## Unity Repository Structure

The Unity project lives at the repository root:

- `Assets/Scripts/` — gameplay code, organized by system
- `Assets/Scenes/` — scenes (test course, prototype)
- `Assets/Prefabs/` — vehicle parts, track prefabs
- `Assets/Art/`, `Assets/Audio/` — binaries (tracked via Git LFS)
- `Packages/` — Unity package manifest
- `ProjectSettings/` — Unity project settings
- `docs/` — project documentation (existing layout, unchanged)

Initial script layout mirrors the MVP development order:

- `Scripts/Vehicle/` — vehicle movement; physics-driven controller
- `Scripts/Building/` — part placement, attachment, validation
- `Scripts/Race/` — race state, checkpoints, finish detection, reset
- `Scripts/UI/` — HUD, build interface (added when UI work starts)

## System Responsibilities

Initial responsibilities, kept small and single-purpose:

- **VehicleController** — reads input, drives the assembled vehicle through its four wheel models (raycast suspension + friction-limited contact). Owns no race or building rules.
- **VehiclePhysics** — pure vehicle math (engine power curve, drag/downforce, traction limits, weight transfer); no UnityEngine dependency (see Vehicle Physics section).
- **BuildValidator** — determines whether an assembly is race-ready (minimum: chassis + wheels + engine, per MVP).

## Vehicle Physics

**Confirmed 2026-08-09** (see `DECISIONS.md` — Vehicle Physics Complexity)

Engineer-grade simcade: real car relationships, tuned for fun.

- **Part specs drive physics.** Parts carry real-derived numbers (engine kW,
  tire μ, chassis mass/Cd — kart class). `VehicleAssembly` combines attached
  parts into an `AssembledVehicle` that configures the controller. Build
  choices have real consequences (BLD-4).
- **Per-wheel raycast suspension** (`WheelModel`): spring + damper per wheel.
  Normal load comes from spring compression, so weight transfer (accel/brake/
  corner load shift) EMERGES — no lookup tables.
- **Friction-limited contact (friction circle).** Each wheel's longitudinal +
  lateral demand is capped at μ × load. Exceed grip → slide; front grip loss =
  understeer, rear grip loss = oversteer/spin. No yaw clamping.
- **Engine model.** Drive force = power / speed (kart CVT approximation),
  traction-capped by driven-wheel grip. Top speed emerges when drive = drag
  (0.5 ρ Cd A v²). No artificial speed cap.
- **Aero.** Drag ∝ v²; downforce ∝ v² (Cl = 0 in MVP — future aero parts).
- **RaceState** — match flow: build phase → countdown → race → results.
- **FinishDetector** — trigger-based finish-line detection.
- **Input** — Unity Input System; keyboard/mouse and gamepad supported from the start.

## Data Flow: Build to Race

1. Player places parts (part data from a ScriptableObject part catalog).
2. BuildValidator checks the assembly; invalid builds show clear feedback.
3. On validation, the vehicle is driven by a dynamic Rigidbody.
4. RaceState runs the countdown; VehicleController drives the Rigidbody.
5. FinishDetector reports the crossing; RaceState shows results and resets.

## Persistent vs Temporary State

- **Temporary:** build assembly, race state — in-memory scene state.
- **Persistent (later):** cosmetics, unlocks, settings — Steam Cloud (planned for P3; not part of the prototype).

## Testing Approach

- Unity Test Framework: EditMode tests for pure logic (validation rules, race state transitions); PlayMode tests for vehicle movement.
- Manual playtest checklist for the build → drive → finish loop.
