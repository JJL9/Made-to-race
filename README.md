# Made to Race

**Made to Race** is a multiplayer racing/building game being developed by three brothers.

Players see a course, build a vehicle for its challenges under time pressure, then immediately race what they built.

## Core Loop

**See the challenge → Build for it → Race it → Learn → Repeat**

Vehicle design should meaningfully affect racing performance. Different courses should reward different builds, and unusual or imperfect vehicles should sometimes produce memorable successes and failures.

The project is being developed with a strong focus on proving the core gameplay before expanding scope.

## Current MVP

The immediate playable goal is:

**Build a basic car → Drive it → Cross a finish line**

The MVP is intended to answer one central question:

> Is building a vehicle and immediately racing it fun?

Work that does not help prove this core loop should generally be deferred until the prototype is working.

## Development Status

**Status:** Early pre-production / prototype setup

Current priorities:

1. Establish repository documentation and team workflow. — **Done.**
2. Choose the game engine/platform. — **Done: Unity 6 LTS + Steam (2026-08-09).**
3. Adapt the repository to the selected engine.
4. Build the first playable vertical slice.

**Engine / Platform — Confirmed (2026-08-09)**

Development uses **Unity 6 LTS**, publishing to **Steam**. Engine-specific structure and conventions are documented in [Architecture](docs/ARCHITECTURE.md); the product plan is in [PRD](docs/PRD.md).

## Project Source of Truth

**GitHub is the source of truth for project code and accepted documentation.**

ChatGPT, Codex, discussions, and brainstorming may help develop ideas, but decisions that affect the project should be recorded in the repository.

When information conflicts, prefer the current repository code and documentation over older discussions.

## Development Approach

The project is maintained by a three-person development team.

General workflow:

* Keep `main` stable and preferably playable.
* Use focused feature or fix branches for development.
* Prefer small, reviewable pull requests.
* Avoid unrelated changes in the same PR.
* Coordinate ownership of major systems to reduce conflicting work.
* Prototype the smallest useful version before expanding scope.
* Do not treat brainstorming as a finalized design decision.

See [Team Workflow](docs/TEAM_WORKFLOW.md) for the full collaboration process.

## Project Documentation

Project knowledge is organized in the following documents:

* [CODEX.md](CODEX.md) — Instructions and working rules for ChatGPT/Codex development sessions.
* [Game Overview](docs/GAME_OVERVIEW.md) — High-level game concept, principles, and intended player experience.
* [PRD](docs/PRD.md) — Product requirements: vision, features, milestones, and Steam platform plan.
* [MVP](docs/MVP.md) — Current prototype scope and explicit non-goals.
* [Team Workflow](docs/TEAM_WORKFLOW.md) — Git, branching, ownership, pull request, and collaboration practices.
* [Decisions](docs/DECISIONS.md) — Confirmed project decisions and important open decisions.
* [Tasks](docs/TASKS.md) — Current development priorities and work ownership.
* [Architecture](docs/ARCHITECTURE.md) — Technical architecture and system boundaries once established.

Some of these documents may not exist yet while the initial repository setup is being completed.

## Scope

The immediate focus is proving the core build-and-race experience.

Major future systems should not be allowed to delay the first playable prototype. Architecture and features should remain as simple as practical until the core gameplay has been validated.
