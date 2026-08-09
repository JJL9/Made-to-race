# MVP

## Purpose

The Made to Race MVP exists to answer one question:

> **Is building a vehicle and immediately racing it fun?**

The MVP should prove the core gameplay loop before the project expands into multiplayer systems, progression, content pipelines, or advanced vehicle simulation.

## MVP Goal

**Confirmed**

The first playable goal is:

**Build a basic car → Drive it → Cross a finish line**

A successful MVP should allow a player to:

1. Enter a simple test environment.
2. Build a basic functional vehicle.
3. Drive that vehicle.
4. Reach and cross a finish line.
5. Reset and try again.

The implementation should be as small as practical while still proving the experience.

## Core MVP Systems

### Vehicle Building

**Confirmed**

The player must be able to construct a basic drivable vehicle.

The minimum useful vehicle is expected to include:

- a chassis
- wheels
- an engine or power source

The exact attachment system, controls, validation rules, and building interface are still unresolved.

### Driving

**Confirmed**

The player must be able to control and drive the vehicle they built.

Driving should provide enough physical response that vehicle construction choices can eventually affect handling and performance.

The first implementation does not need advanced simulation.

### Test Course

**Confirmed**

The MVP requires one simple test course or environment.

Its purpose is to test building and driving, not to demonstrate final map quality.

The course should include:

- a starting area
- enough space to drive
- a finish line

Additional obstacles or terrain should only be added when needed to test meaningful vehicle behavior.

### Finish Detection

**Confirmed**

The game must detect when the player's vehicle crosses the finish line.

A simple completion result is sufficient for the first playable version.

### Reset / Retry

**Confirmed**

The player should be able to reset the test and quickly build or drive again.

Fast iteration is important for evaluating whether the core loop is enjoyable.

## MVP Development Order

**Proposed**

Develop the MVP in this order:

1. Basic driving and vehicle movement
2. Basic vehicle construction
3. Vehicle validation
4. Simple test course
5. Finish detection
6. Reset / retry loop
7. Minimal build-to-race flow

Each step should remain playable and testable where practical.

## MVP Success Criteria

The first playable prototype should demonstrate that:

- a player can build a basic vehicle
- the constructed vehicle can be driven
- building choices can begin to influence vehicle behavior
- the player can complete a simple course
- the build-and-drive loop can be repeated quickly
- the experience is promising enough to justify expanding the prototype

Polish is not required to satisfy these criteria.

## Explicit Non-Goals

The following should **not** block the MVP:

- community map editor
- multiple polished maps
- large parts catalog
- large cosmetics catalog
- progression systems
- shops
- battle passes
- ranked modes
- tournaments
- wagering
- complex matchmaking
- advanced destruction
- advanced damage systems
- sophisticated backend infrastructure
- large-scale content pipelines
- extensive optimization
- final visual polish

These may be considered later only after the core gameplay has been validated.

## Multiplayer

**Confirmed**

Made to Race is intended to be multiplayer-first in the long term.

However, full multiplayer implementation is **not required to prove the first local playable loop**.

The MVP architecture should avoid obvious choices that would make future multiplayer unnecessarily difficult, but networking should not be over-engineered before the local build-and-drive experience works.

## Vehicle Complexity

The prototype should begin with the smallest vehicle system that produces useful gameplay.

Possible future systems include:

- suspension
- aerodynamics
- durability
- structural damage
- weight distribution
- traction
- torque
- advanced drivetrain behavior
- complex attachment rules

These systems are **not confirmed MVP requirements**.

They should only be introduced when there is a clear gameplay reason to test them.

## Open Decisions

The following decisions are unresolved and should not be assumed during MVP implementation:

- **Open Decision — Game engine/platform**
- **Open Decision — Target platform(s)**
- **Open Decision — Camera approach**
- **Open Decision — Building controls**
- **Open Decision — Part placement and attachment system**
- **Open Decision — Vehicle validation rules**
- **Open Decision — Vehicle physics complexity**
- **Open Decision — Damage/destruction**
- **Open Decision — Exact build phase duration**
- **Open Decision — Exact race flow**
- **Open Decision — Networking implementation**

Any implementation depending heavily on one of these choices should flag the dependency instead of silently deciding it.

## Scope Rule

When deciding whether something belongs in the MVP, ask:

> Does this help us determine whether building a vehicle and immediately driving/racing it is fun?

If the answer is no, it should normally be deferred.

The MVP should remain focused on:

**Build a basic car → Drive it → Cross a finish line**
