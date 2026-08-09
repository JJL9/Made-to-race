# Architecture

## Purpose

This document records the technical architecture and major system boundaries for Made to Race.

It should describe accepted implementation structure once those decisions exist.

Do not use this file to speculate about architecture that has not been selected.

## Current Status

**Open Decision — Game Engine / Platform**

The game engine and development platform have not yet been selected.

Because of that, the following are intentionally not defined yet:

- engine-specific folder structure
- programming language
- scene or object structure
- physics implementation
- networking framework
- serialization approach
- build/deployment process
- platform-specific systems

These should be documented only after the relevant decisions are made.

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

## Next Architecture Step

After the game engine/platform is selected, this document should be expanded to cover:

- repository structure
- major gameplay systems
- system responsibilities
- dependencies and data flow
- vehicle simulation boundaries
- build-to-race flow
- multiplayer boundaries
- persistent versus temporary state
- testing approach

Until then, keep this document engine-agnostic.
