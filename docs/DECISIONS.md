# Decisions

## Purpose

This file records important confirmed decisions for Made to Race.

Chat discussions, brainstorming, and temporary implementation choices are not considered final project decisions unless they are recorded here or in another authoritative repository document.

Use the following labels:

- **Confirmed** — accepted team decision
- **Proposed** — current recommendation or direction, not yet finalized
- **Open Decision** — unresolved
- **Future Idea** — intentionally outside the current MVP

## Confirmed Decisions

### Project Source of Truth

**Confirmed**

GitHub is the source of truth for:

1. Current code
2. Current repository documentation
3. Accepted project decisions

If chat context conflicts with current repository code or documentation, the repository takes priority unless the team explicitly decides to change it.

### Core Game Concept

**Confirmed**

Made to Race is a multiplayer racing/building game where players:

**See the challenge → Build for it → Race it → Learn → Repeat**

Players should build vehicles for the course rather than simply selecting finished vehicles.

### Current MVP

**Confirmed**

The immediate playable goal is:

**Build a basic car → Drive it → Cross a finish line**

The MVP exists to answer:

> Is building a vehicle and immediately racing it fun?

Features that do not help answer this question should generally be deferred.

### Development Approach

**Confirmed**

The project should prioritize:

- small, focused changes
- feature branches for substantial development work
- small pull requests
- keeping `main` stable and preferably playable
- one primary owner per major active system where practical
- minimal implementation before expansion
- avoiding premature optimization and abstraction

### Progression Direction

**Confirmed**

Progression should avoid permanent competitive advantages based primarily on time played or spending.

Preferred direction:

**Played longer = more experienced builder/driver + more customization**

Progression should primarily support cosmetics, identity, mastery, achievements, and player expression.

### Community Map Creator

**Future Idea**

A community map/track creator is a major long-term feature idea.

It is not part of the first prototype or MVP.

## Proposed Directions

### Build Phase Duration

**Proposed**

A build phase of roughly two minutes has been discussed.

The exact duration is not finalized.

### Player Count

**Proposed**

A multiplayer match size of roughly 8–12 players has been discussed.

The final player count is unresolved.

### Minimum Vehicle

**Proposed**

A minimum functional vehicle may consist of:

- chassis
- wheels
- engine or power source

The exact part rules and attachment system are unresolved.

### Weather & Track Conditions

**Proposed**

Weather conditions as course modifiers (Rain, Windy, Cold, Clear) are a candidate direction for multiplying build tradeoffs without new course geometry. Conditions would be announced during course inspection, be session-consistent, and each require a visible effect, a build counter, and a skill counter.

Not part of the MVP. Design direction recorded in `docs/GAME_OVERVIEW.md`; requirements in `docs/PRD.md` (§6.9).

## Open Decisions

The following decisions are currently unresolved.

### Game Engine / Platform

**Confirmed — 2026-08-09**

Unity is the selected game engine. Development targets **Unity 6 LTS** (the current 6.x LTS at project setup; the exact patch version is pinned when the Unity project is created and recorded in `docs/ARCHITECTURE.md`).

Rationale: mature physics and vehicle tooling, a well-supported Steam integration path (Steamworks.NET), broad platform coverage including Steam Deck, and a documentation/tooling ecosystem well suited to a three-person team.

Engine-specific structure, physics approach, and system boundaries are documented in `docs/ARCHITECTURE.md`.

### Target Platform(s)

**Confirmed — 2026-08-09**

Steam is the initial target platform (Windows first, with Steam Deck support as a stated goal). Store page, Steamworks, and publishing requirements are tracked in `docs/PRD.md` (§5).

Other platforms are not ruled out but are not part of the current plan.

### Camera

**Open Decision**

The gameplay camera approach has not been selected.

### Building Controls

**Open Decision**

The exact interaction model for placing, moving, rotating, attaching, and removing vehicle parts has not been selected.

### Part Attachment System

**Open Decision**

The rules for how vehicle parts connect to each other have not been finalized.

### Vehicle Validation

**Open Decision**

The rules that determine whether a constructed vehicle is valid and race-ready have not been finalized.

### Vehicle Physics Complexity

**Confirmed 2026-08-09** — engineer-grade (simcade)

Physics and mechanics model real car systems, as if a car mechanic and
design engineer built the game (owner decision 2026-08-09):

- **Engine:** real power curve; force falls off with speed; top speed
  EMERGES from power vs drag — no artificial speed cap.
- **Tires:** grip is a friction limit (μ × tire load, friction circle).
  Sliding, understeer, and oversteer EMERGE from exceeding grip — no yaw
  clamping or spin-prevention cheats.
- **Weight:** power-to-weight drives acceleration; weight transfer (load
  shift under accel/brake/corner) makes weight matter in corners.
- **Aero:** drag and downforce ∝ speed² (downforce Cl = 0 in MVP).
- **Parts** carry real-derived specs (engine kW, tire μ, chassis mass/Cd)
  that feed the physics — build choices have real consequences (BLD-4).
- **Suspension:** per-wheel raycast springs (rest length, stiffness,
  damper) — normal load and weight transfer come from spring compression.

Fun and readable gameplay still takes priority over strict realism
(simcade, Forza/GT territory — not a full sim). Reference data: kart-class
vehicle (~177 kg with driver, 15 kW ≈ 20 hp, μ ≈ 1.0 dry asphalt,
Cd ≈ 0.7, A ≈ 0.9 m²) → ~120 km/h top speed, ~0.7g launch.

Supersedes: the arcade tuning pass (commit 2161275) and the body-driven
force model from M0-2 (yaw-rate clamping removed as a stability crutch).

### Damage / Destruction

**Open Decision**

The role of vehicle damage, breakage, and destruction has not been finalized.

### Exact Race Format

**Open Decision**

The final race structure, lap format, checkpoint behavior, and related rules have not been finalized.

### Multiplayer Networking

**Open Decision**

The networking architecture and authoritative multiplayer model have not been selected.

### Progression Details

**Open Decision**

The exact progression, unlock, achievement, and cosmetic systems have not been designed.

### Monetization

**Open Decision**

No monetization model has been finalized.

## Decision Process

When a significant decision becomes final:

1. Confirm it with the team.
2. Update this file.
3. Update any affected documentation.
4. Update architecture documentation if the decision changes technical structure.

Do not record every small implementation detail here.

This file should contain decisions that materially affect game design, architecture, scope, workflow, or future implementation.
