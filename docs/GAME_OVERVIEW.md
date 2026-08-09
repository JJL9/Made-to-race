# Game Overview

## Project

**Made to Race** is a multiplayer racing/building game being developed by three brothers.

Players do not simply choose a finished vehicle. They see the course, build a vehicle for its challenges under time pressure, and then immediately race what they created.

## Core Loop

**See the challenge → Build for it → Race it → Learn → Repeat**

The central goal is for both building and driving skill to matter.

Vehicle design choices should produce visible consequences during the race, and different courses should reward different approaches rather than one universally optimal vehicle.

## Core Design Principles

**Confirmed**

- Easy to learn, difficult to master.
- Building decisions should meaningfully affect racing.
- Driving skill should remain important.
- Different courses should create different vehicle-building tradeoffs.
- Strange or unconventional designs should sometimes succeed.
- Funny and emergent failures are valuable parts of the experience.
- Player knowledge and skill should matter more than permanent stat upgrades.
- Multiplayer should create memorable moments.
- Prototype the core experience before expanding scope.
- Fun is more important than strict simulation realism.

## Intended Match Flow

**Proposed**

The current direction for a match is:

1. Players join.
2. A course is shown.
3. Players inspect the course and its major challenges.
4. A timed build phase begins.
5. Players construct their vehicles.
6. Valid vehicles move to the starting area.
7. A countdown begins.
8. Players race.
9. Results are shown.
10. The match resets or proceeds to another course.

**Open Decision — Build Phase Duration**

A build phase of roughly two minutes has been discussed, but the exact duration is not finalized.

## Vehicle Building

The building system should be understandable for new players while allowing experienced players to make increasingly effective designs.

A minimum functional vehicle may consist of:

- chassis
- wheels
- engine or power source

Additional vehicle systems should only be added when they improve the core gameplay.

Possible later systems include suspension, aerodynamics, durability, weight distribution, traction, torque, stability, structural parts, and cosmetics.

These are not automatically part of the MVP.

## Course Design

Courses should create meaningful vehicle-design tradeoffs.

Different layouts and obstacles should make different vehicle characteristics useful so that players must respond to the course rather than repeatedly build the same optimal vehicle.

The exact course types and launch map count are not finalized.

## Weather & Track Conditions

**Proposed**

Weather conditions are a strong candidate for multiplying course variety: the same course under different conditions becomes a different build problem, with no new geometry required.

Design direction under discussion:

- Conditions are announced during course inspection (a race-day briefing), before the build phase — players always build with full information.
- One dominant condition per match initially: **Clear, Rain, Windy, or Cold**. Conditions are session-consistent; no mid-race surprises in the default mode.
- Every condition should have a visible effect, a build counter, and a skill counter:

| Condition | Build response | Skill response |
|---|---|---|
| Rain | rain tires (wet grip) vs slicks (dry grip) | line choice around puddles, aquaplaning |
| Windy | aero parts (stability vs weight/drag tradeoff) | reading gusts via visual telegraphs |
| Cold | tire compound (soft grips cold but overheats; hard fast when warm, risky lap 1) | managing tire warmup on lap 1 |
| Clear | baseline — speed/agility builds | pure racing line |

- Weather is **not** part of the MVP. It is a candidate for the first P1/P2 systems because it is mostly parameter modulation (grip multipliers, wind force, visuals) on top of the physics already being built.
- Long term, community-created maps could ship with condition presets (**Future Idea**).

## Multiplayer

**Confirmed**

Made to Race is intended to be multiplayer-first.

Multiplayer should amplify the core build-and-race experience through competition, unusual vehicle designs, mistakes, recoveries, and unexpected outcomes.

**Open Decision — Player Count**

A rough range of 8–12 players has been discussed, but the final player count has not been decided.

The prototype should not over-engineer multiplayer before the local core gameplay works.

## Progression

**Confirmed**

Progression should not follow:

**Played longer = permanently stronger vehicle**

The preferred direction is:

**Played longer = more experienced builder/driver + more customization**

Progression should primarily reward:

- cosmetics
- identity
- mastery
- achievements
- player expression

Permanent pay-to-win performance advantages should be avoided.

The exact progression system is not yet defined.

## Future Ideas

**Future Idea — Community Map Creator**

A community track/map creation system is a major long-term feature idea.

It is not required for the first prototype or MVP.

Other major game modes, ranked systems, tournaments, advanced progression, large content catalogs, and similar features should also wait until the core gameplay is proven.

## Current MVP Goal

The immediate playable goal is:

**Build a basic car → Drive it → Cross a finish line**

The MVP should answer:

> Is building a vehicle and immediately racing it fun?

See [`MVP.md`](MVP.md) for the detailed MVP scope.

## Open Decisions

Important unresolved decisions currently include:

- Game engine/platform
- Target platform(s)
- Final player count
- Camera approach
- Building controls
- Vehicle physics complexity
- Part attachment system
- Damage/destruction
- Race format details
- Launch map count
- Progression details
- Monetization
- Networking implementation

Do not treat these as decided until they are recorded as confirmed project decisions.
