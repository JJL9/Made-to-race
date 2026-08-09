# CODEX.md

This file defines the operating rules for ChatGPT/Codex sessions working on **Made to Race**.

The goal is to keep AI-assisted development consistent, scoped, reviewable, and aligned with the repository.

## Source of Truth

GitHub is the source of truth for:

1. Current code
2. Current repository documentation
3. Confirmed project decisions

Chat discussions and brainstorming are supporting context, not authoritative project state unless their decisions have been recorded in the repository.

## Before Making Changes

Before modifying the repository:

1. Read the relevant project documentation.
2. Inspect the existing implementation.
3. Check the current branch and active task.
4. Identify which systems/files are likely to change.
5. Check whether another developer is already working on the same major system.
6. Confirm the smallest testable outcome for the task.

Do not propose replacing an existing system before understanding how it currently works.

## Engine / Platform

**Confirmed — 2026-08-09**

* Engine: Unity 6 LTS
* Language: C#
* Distribution: Steam

Unity-specific project structure, physics approach, and system boundaries are documented in `docs/ARCHITECTURE.md`. Follow the repository structure and boundaries there instead of inventing new layouts.

Networking framework and deployment specifics remain open decisions until multiplayer work begins.

## Task Ownership and Git Awareness

Made to Race is developed by three developers who may work in parallel.

Before starting substantial work:

* Verify the intended branch.
* Understand the task being worked on.
* Avoid editing a major system currently owned by another developer unless coordination has occurred.
* Prefer one primary owner per active major system where practical.

Recommended branch types:

* `feature/...`
* `fix/...`
* `docs/...`

Keep `main` stable and preferably playable.

Do not make broad unrelated changes simply because they are convenient while working on another task.

## Scope Discipline

Keep every change tightly aligned with the requested task.

Avoid:

* unrelated refactors
* unnecessary rewrites
* speculative features
* premature abstraction
* premature optimization
* large manager classes or scripts that accumulate unrelated responsibilities
* introducing dependencies without a clear need
* silently changing architecture while implementing a feature

If a larger refactor appears necessary, explain why before treating it as part of the task.

Prefer small, focused, reviewable changes.

## Open Decisions

Do not silently finalize unresolved design questions.

Use these labels when relevant:

* **Confirmed** — already accepted project decision
* **Proposed** — recommendation that has not yet been accepted
* **Open Decision** — unresolved question requiring a team decision
* **Future Idea** — intentionally outside the current MVP

When implementation requires an unresolved decision, either:

1. avoid depending on that decision, or
2. clearly flag the dependency as an **Open Decision**.

Do not invent project requirements to fill missing information.

## Implementation Philosophy

Prefer the **smallest testable implementation** that proves the required behavior.

For the current project phase, prioritize proving:

**Build a basic car → Drive it → Cross a finish line**

Do not expand a task into systems that are unnecessary for this goal.

Code should be:

* readable
* modular
* easy to test
* easy for another developer to understand
* minimally dependent on unrelated systems

Prefer clear code over clever code.

## System Boundaries

Where practical, avoid tightly coupling unrelated responsibilities.

Keep clear boundaries between:

* gameplay rules
* player input
* UI
* visuals and presentation
* vehicle physics/simulation
* race state
* networking
* persistent data

These boundaries do not require elaborate frameworks or excessive abstraction.

Use the simplest structure that keeps systems understandable and prevents obvious future problems.

## Multiplayer Considerations

Made to Race is multiplayer-first long term, but the prototype does not require every system to be fully networked immediately.

When designing gameplay systems:

* consider which state may eventually need multiplayer synchronization
* avoid architecture that makes future networking unnecessarily difficult
* separate authoritative gameplay state from purely visual state where practical

Do **not** build complex networking infrastructure before the local core gameplay requires it.

Future multiplayer concerns should influence clean boundaries, not cause premature over-engineering.

## Testing

Every code change should be tested at the smallest practical level.

Before considering work complete:

1. Verify the requested behavior works.
2. Check directly affected existing behavior for regressions.
3. Test important failure or edge cases where practical.
4. Run existing automated tests or validation tools relevant to the changed system, if available.
5. Provide manual testing steps when automated coverage is unavailable or insufficient.

Do not claim something was tested if it was not.

If testing could not be completed, state exactly what remains unverified.

## Repository Conflicts

If the user's request conflicts with current repository code, documentation, or a confirmed decision:

1. Do not silently override the repository.
2. Identify the conflict clearly.
3. Explain which repository source conflicts with the request.
4. Ask for or propose an explicit decision when needed.

If code and documentation conflict with each other, point out the discrepancy rather than guessing which one is correct.

Repository documentation should be updated when an accepted change makes existing documentation inaccurate.

## Documentation

Update repository documentation when a change establishes or alters:

* a major game-design decision
* architecture
* system ownership or boundaries
* development workflow
* important technical constraints
* MVP scope

Important accepted decisions should be recorded in:

`docs/DECISIONS.md`

Architecture changes should be reflected in:

`docs/ARCHITECTURE.md`

Do not turn every minor implementation detail into a formal decision entry.

## After Making Changes

After completing repository changes, report:

* **Task completed**
* **Branch used**
* **Files changed**
* **What changed**
* **What was tested**
* **Manual test steps**, if relevant
* **Known bugs, risks, or limitations**
* **Open Decisions discovered**
* **Documentation that was updated or should be updated**
* **Recommended next task**, when useful

For significant architectural changes, also explain:

* why the chosen structure was used
* which systems depend on it
* important tradeoffs
* multiplayer implications

Keep the report factual and scoped to the work performed.

## Core Rule

Do not treat brainstorming as finalized.

Inspect first. Change only what is necessary. Test what you change. Document accepted decisions. Keep the project moving toward a playable core loop.
