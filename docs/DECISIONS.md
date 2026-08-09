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

**Open Decision**

The game engine and development platform have not been selected.

Do not assume engine-specific:

- folder structure
- programming language
- physics system
- networking framework
- tooling
- deployment process

until this decision is confirmed.

### Target Platform(s)

**Open Decision**

The initial target platform or platforms have not been finalized.

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

**Open Decision**

The desired balance between arcade physics and simulation complexity has not been finalized.

Fun and readable gameplay should take priority over strict realism.

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
