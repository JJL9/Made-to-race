# Made to Race — Product Requirements Document (PRD)

| Field | Value |
|---|---|
| Status | **Draft 0.1** — for team review |
| Date | 2026-08-09 |
| Owner | Development team (three brothers) |
| Scope | Prototype → Steam launch (Unity) |
| Related docs | [Game Overview](GAME_OVERVIEW.md) · [MVP](MVP.md) · [Decisions](DECISIONS.md) · [Architecture](ARCHITECTURE.md) · [Tasks](TASKS.md) |

**Labels used in this document** (matching the repo convention): **Confirmed** — accepted team decision · **Proposed** — current direction, not yet finalized · **Open Decision** — unresolved · **Future Idea** — intentionally outside current scope.

---

## 1. Overview

### 1.1 Vision

**Made to Race** is a multiplayer racing/building game where players see a course, build a vehicle for it under time pressure, and immediately race what they built.

The hook: the vehicle you race is the vehicle you *just* designed. The tension and comedy come from the gap between intent and result — a "perfect" build can dominate, a junkyard build can somehow win, and glorious failures are part of the fun. **Confirmed** (per `GAME_OVERVIEW.md`).

### 1.2 Core Loop

> **See the challenge → Build for it → Race it → Learn → Repeat** — **Confirmed**

### 1.3 Product Pillars

Derived from the confirmed design principles in `GAME_OVERVIEW.md`:

1. **Building decisions matter** — design choices visibly change race performance; no universally optimal vehicle; different courses reward different builds.
2. **Driving skill matters** — a good build does not win by itself.
3. **Emergent comedy** — strange designs, failures, and recoveries are content, not bugs.
4. **Multiplayer memories** — competition, chaos, and unexpected outcomes amplify the core loop (multiplayer-first long term; local core first).
5. **Mastery over grinding** — progression rewards experience, identity, and expression; permanent stat power from time played or spending is avoided.

### 1.4 North-Star Question

> **Is building a vehicle and immediately racing it fun?**

Every requirement below is subordinate to proving this with the smallest playable slice first (§7), per the repo's confirmed scope rule.

---

## 2. Goals & Success Metrics

### 2.1 Phase Goals

| Phase | Goal | Primary success signal |
|---|---|---|
| P0 — Prototype (MVP) | Prove local *build → drive → finish* is fun | Playtest verdict + MVP success criteria (§7) |
| P1 — Core loop depth | Course & part variety create real build tradeoffs | "One more race" rate in internal playtests |
| P2 — Multiplayer proof | 4–8 players build and race together without jank | Cross-session playtest retention |
| P3 — Steam launch | Discoverable, positively reviewed launch | Wishlists, review score, CCU, retention |

### 2.2 Steam-Era KPIs

**Proposed** targets — placeholders the team should own and validate; revisit after P1 playtest data.

| Metric | Proposed target | Notes |
|---|---|---|
| Store page lead time | Live ≥ 90 days before launch | Longer lead = more wishlist farming |
| Wishlists at launch | ≥ 10k | Rough indie baseline for meaningful launch visibility |
| Wishlist → first-week purchase | ~10–20% conversion | Typical indie range; influenced by price & demo |
| Launch-week peak CCU | 300–1,000 | Scaled by wishlist conversion; not a hard gate |
| Reviews (first 30 days) | ≥ 80% positive | Steam algorithm + store visibility threshold |
| Week-1 refund rate | < 15% | High refunds signal expectation mismatch (see §10) |
| 30-day return rate | ≥ 20% of buyers | "Came back for another race" proxy |

---

## 3. Target Audience & Positioning

### 3.1 Audiences

**Primary — "builder-racers"** (physics construction + racing fans):
- Players of *Besiege, Trailmakers, Scrap Mechanic, Banjo-Kazooie: Nuts & Bolts, From the Depths* who enjoy designing vehicles and seeing them perform.
- Arcade racing players (*Trackmania, Wreckfest, Mario Kart*) who value speed, flow, and party energy.

**Secondary — streamers & content creators:**
- Short match length (~5–8 min) and emergent comedy make the game naturally watchable and clip-able. Building *under time pressure* creates built-in drama and audience participation ("build it like this!").
- Co-op/couch play (local multiplayer, Remote Play Together) supports creator groups.

**Demographic:** broad, teen+, both keyboard/mouse and gamepad; no age-specific content. Not a sim audience — fun beats realism (**Confirmed**).

### 3.2 Positioning Statement

> For players who love building *and* racing, Made to Race is the only game where the car you race is the car you built **two minutes ago** — under pressure, for a course you just saw. Unlike pure builders, you must perform; unlike pure racers, your vehicle is never a given.

### 3.3 Why Steam

- The multiplayer/creator-game audience is concentrated on Steam; Workshop (Future Idea — community map creator) is a proven discoverability engine on the platform.
- Steam's playtest, demo/Next Fest, and community tools fit the team's "prove it before expanding" approach.
- Solo/small-team publishing is well-trodden ($100 Steam Direct fee, no revenue split beyond Valve's 30% / tiered at $10M+).

---

## 4. Core Experience

### 4.1 Intended Match Flow — **Proposed** (from `GAME_OVERVIEW.md`, with durations)

| Step | Phase | Proposed duration |
|---|---|---|
| 1 | Players join (lobby/match setup) | ~60s |
| 2 | Course is shown | — |
| 3 | Players inspect the course and its major challenges | ~30s |
| 4 | Timed **build phase** begins | ~120s (see §4.2) |
| 5 | Players construct their vehicles | (in build phase) |
| 6 | Valid vehicles move to the starting area | ~10s |
| 7 | Countdown | 5s |
| 8 | Race | 2–5 min |
| 9 | Results shown | ~20s |
| 10 | Reset or proceed to another course | — |

**Target match length: ~5–8 minutes.** Session target: 3–5 matches per sitting with an easy "one more race" path.

### 4.2 Build Phase — **Open Decision** (duration)

- Roughly 2 minutes has been discussed (**Proposed**).
- Requirements to test in P1: Does the duration allow *meaningful* decisions without dead time? Does pressure create fun tension or frustration? Measure both.
- The build interface must be learnable in the first match (see §6.1) — pressure only works if players know what they're doing.

### 4.3 Race Phase

- Driving must respond clearly to construction choices: weight, wheel count/placement, power, and (later) suspension and aero should produce readable differences in handling (**Confirmed** principle).
- Racing line, obstacles, and course features should reward course-specific builds — a car that wins course A should be at a real disadvantage on course B.
- Failure is part of the experience: rollovers, breakage, and undriveable builds should be spectacular and funny, not just punishing.

### 4.4 Emergent Comedy as Content

- The game should *actively* support memorable failures: visible physics, funny part combinations, recoveries, and photo-finishes.
- This is a design requirement, not a nice-to-have — it is the game's marketing engine (clips, streams, screenshots, reviews).

---

## 5. Platform & Distribution

### 5.1 Engine Decision: Unity — **Confirmed (direction, 2026-08-09)**

The team direction is **Unity** for development, publishing to **Steam**.

> ⚠️ **Repo process note:** the "Game Engine / Platform" and "Target Platform(s)" entries in `DECISIONS.md` are still **Open Decision**. When the team formally accepts Unity + Steam, record it there and update `ARCHITECTURE.md` (per the documented decision process) — this unblocks engine-specific structure, physics, networking, and build docs.

### 5.2 Unity Configuration (Proposed baseline)

| Item | Recommendation | Rationale |
|---|---|---|
| Version | **Unity 6 LTS** — pin the current 6.x LTS at project creation; stay on LTS | Long support window; no mid-project tech-stream churn |
| Render pipeline | **URP** | Stylized art, strong performance headroom for 8–12 players and Steam Deck |
| Scripting | C# (Mono/IL2CPP for release; IL2CPP for Steam build) | Standard Unity path |
| Input | Unity **Input System** package + Steam Input for rebinding | Gamepad/keyboard parity from day one (P1 requirement) |
| Version control | Git (existing repo) + **Git LFS** for art/audio binaries; Unity `.gitignore` | Team of 3 + AI sessions; keep repo light |
| Physics | Unity built-in (PhysX) for P0–P1; **arcade-first tuning** | Fun/readability over realism (**Confirmed**); revisit only if a gameplay need appears |
| Netcode (when needed) | Unity **Netcode for GameObjects** (official) or **Mirror**, over **Steam Networking Sockets** P2P transport | Mature, well-trodden Steam path; decision recorded at P2 |

### 5.3 Physics & Vehicle Simulation Approach

- **P0–P1:** Rigidbody + wheel colliders (or simple custom raycast suspension) tuned for readable arcade handling. Enough fidelity that part choices matter; no simulation rabbit holes (**Confirmed** MVP non-goal).
- **Open Decision — complexity ceiling:** how far suspension/aero/durability/weight-distribution go. Rule of thumb from `MVP.md`: add a system only when a course design *requires* it to create a new tradeoff.
- **Open Decision — vehicle validation rules:** what makes a build race-ready (minimum chassis/wheels/engine, driveability sanity, part-limit caps for perf).

### 5.4 Steam Publishing Requirements (Confirmed platform facts)

- **Steam Direct:** one-time **$100 fee** per product (recoupable after the game passes $1,000 in adjusted gross revenue). App created under an individual/company Steamworks account.
- **Steamworks SDK** via **Steamworks.NET** (C# bindings — the standard Unity integration).
- **Build pipeline:** SteamPipe/`steamcmd` depots; default + beta + playtest branches.
- **Store page assets** (required before page approval): header capsule 460×215, small capsule 231×87, library header 616×353, library hero, logo 600×315, page background, ≥ 5 screenshots (1280×720+), optional animated capsule & trailer (30–90s).
- **Steam features to plan for** (see §6.6 for requirement detail): Achievements, Cloud Saves, Rich Presence, Screenshots/Overlay, Steam Input, Remote Play Together (couch/local co-op), Community Hub, Workshop (later — ties to the community map creator **Future Idea**).
- **Steam Deck:** target **Deck Verified** — URP perf budget, gamepad UX, 16:10 safe areas, readable UI at 7" (validate during P3).
- **Marketing levers:** Steam **Playtest** (free, fits the P2 closed-testing phase), **Next Fest demo** (wishlist driver pre-launch), **Early Access** (option — see monetization **Open Decision**).

---

## 6. Feature Requirements

Priority key: **P0** = MVP (must prove the core loop) · **P1** = core-loop depth (pre-multiplayer) · **P2** = multiplayer + Steam playtest · **P3** = launch-ready · **P4** = post-launch / **Future Idea**.

### 6.1 Vehicle Building — P0 (core), P1 (depth)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| BLD-1 | P0 | Player can construct a basic drivable vehicle from a chassis, wheels, and an engine/power source | Fresh player builds a valid vehicle in ≤ 2 min in the tutorial/test environment |
| BLD-2 | P0 | Parts attach, detach, and re-position without crashes or soft-locks | All standard part operations work; reset restores a clean state |
| BLD-3 | P0 | Building input is learnable within the first match | First-time playtesters need no instructions beyond brief on-screen hints |
| BLD-4 | P1 | Part catalog expands to create real tradeoffs: weight, power, traction, torque, wheel count/placement | At least two courses where different builds are meaningfully advantaged |
| BLD-5 | P1 | Vehicle validation gives clear, fast feedback on why a build is invalid | Player understands and fixes an invalid build in < 30s |
| BLD-6 | P1 | Build UI works on gamepad and keyboard/mouse | Full build flow usable on both inputs |
| BLD-7 | P2 | Build state syncs correctly across players in multiplayer | All players see identical builds and placements within 150ms |
| BLD-8 | P3 | Cosmetics/decals/paint can be applied without affecting performance (**Confirmed** progression direction) | Cosmetics never alter physics parameters |

**Open Decisions:** part attachment system, building controls, validation rules, build-phase duration.

### 6.2 Vehicle Physics & Driving — P0 (basic), P1 (depth)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| PHY-1 | P0 | Built vehicle is drivable: accelerate, steer, brake | Player controls the vehicle stably on flat ground |
| PHY-2 | P0 | Construction choices produce visible handling differences | Adding/removing weight or changing wheels measurably changes behavior |
| PHY-3 | P1 | Vehicle can fail spectacularly but recoverably (rollover, stuck) | Reset/retry exists; failures are funny, not frustrating |
| PHY-4 | P1 | Arcade-tuned feel: readable, fun, responsive | Internal playtest: "feels good" consensus after tuning passes |
| PHY-5 | P2 | Physics behaves deterministically enough for fair multiplayer races | No desync-induced unfairness in playtests (see §6.5) |
| PHY-6 | P3 | Performance: stable 60 FPS with 8–12 vehicles + build scenes | Profiling gate on reference hardware & Steam Deck |

**Open Decisions:** physics complexity ceiling, damage/destruction role.

### 6.3 Courses — P1 (variety), P3 (content)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| CRS-1 | P0 | One simple test course: start area, driveable space, finish line (**Confirmed**) | Player can build, drive, and cross the line |
| CRS-2 | P1 | 2–3 courses that reward different build philosophies | Playtest shows players changing builds per course |
| CRS-3 | P3 | Launch course set (Proposed: 8–12) with distinct challenge identities | Each course tests ≥ 2 build variables; no dominant universal build |
| CRS-4 | P3 | Checkpoints/race line readable at speed | Players never lose their way mid-race |
| CRS-5 | P4 | Community map creator (**Future Idea** — not before launch) | — |

**Open Decisions:** launch map count, course types, checkpoint/lap format.

Track conditions multiply course variety — see §6.9.

### 6.4 Race Rules & Flow — P0 (minimal), P1 (real format)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| RAC-1 | P0 | Finish-line crossing is detected and reported (**Confirmed**) | Crossing the line shows a completion result |
| RAC-2 | P0 | Reset/retry restarts build-or-drive quickly (**Confirmed**) | Full reset loop < 5s to a clean state |
| RAC-3 | P1 | Full match flow implemented: join → inspect → build → countdown → race → results → next | The §4.1 flow runs end-to-end with timers |
| RAC-4 | P1 | Race format chosen: time trial, lap race, checkpoint rally, or mix | Format recorded as a decision after P1 playtests |
| RAC-5 | P2 | Multi-match session flow (best-of-N, rotation, rematch) | Group of 4+ can play back-to-back matches without host friction |

**Open Decisions:** exact race format, lap/checkpoint rules.

### 6.5 Multiplayer — P2 (first networkable build), P3 (launch)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| MP-1 | P2 | 4–8 players join a shared build-race session (Proposed target: 8–12 eventually) | Full §4.1 flow with remote players; no host-only exploits |
| MP-2 | P2 | Build-phase state sync (see BLD-7) and race-state sync | Playtest without visible jank at target player count |
| MP-3 | P2 | Lobby + session management (host invite via Steam friends) | Steam friends can join via invite/overlay |
| MP-4 | P2 | Authoritative simulation to prevent cheating/desync (host-authoritative or dedicated) | Architecture decision recorded before implementation |
| MP-5 | P3 | Dedicated-server mode or robust host migration | Player-count targets hold without a host quit ending the match |
| MP-6 | P3 | Reconnect & spectator basics | Disconnected player can rejoin or spectate |

**Open Decisions:** player count, networking implementation/model.

### 6.6 Progression & Cosmetics — P3 (launch basics), P4 (depth)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| PRG-1 | P3 | Cosmetic-only progression (paint, decals, body cosmetics) — **Confirmed** direction | No performance-affecting unlockables |
| PRG-2 | P3 | Achievements set (Proposed: 10–20 at launch) via Steam | Achievements unlock and persist correctly |
| PRG-3 | P3 | Steam Cloud saves for settings, cosmetics, unlocks | Progress survives reinstall/Deck switch |
| PRG-4 | P4 | Identity/expression depth: garage, liveries, driver tags | (post-launch) |
| PRG-5 | P4 | Monetization model decided (cosmetics-only shop, DLC, or none) | Decision recorded — see **Open Decision** |

### 6.7 UI/UX — P0 (minimal), P3 (polish)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| UI-1 | P0 | Minimal HUD: drive state, finish result, reset | Player never lost about current state |
| UI-2 | P0 | Build UI communicates part selection & placement | First-time use without external guidance |
| UI-3 | P1 | Build-phase timer visible & legible under pressure | Timer readable at a glance |
| UI-4 | P3 | Main menu, settings, graphics, audio, rebinding (Steam Input) | All ship at launch |
| UI-5 | P3 | Steam Deck UI pass: 16:10, font scaling, touch-friendly menus | Deck Verified checklist items pass |

### 6.8 Audio & Visual — P3 (launch)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| AV-1 | P3 | Stylized art direction chosen and applied consistently | No asset feels out of place; art direction recorded |
| AV-2 | P3 | Audio: engine, collisions, UI, build interactions, music | Audio feedback supports comedy and clarity |
| AV-3 | P3 | Optimization pass: load times, memory, shader budgets | Launch perf gates pass (see PHY-6) |

### 6.9 Weather & Track Conditions — P1 (first condition), P2 (rest) — **Proposed**

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| WTH-1 | P1 | Match condition is announced during course inspection (race-day briefing) before the build phase | Player knows the condition before building; condition is session-consistent for the whole match |
| WTH-2 | P1 | One dominant condition per match initially: Clear, Rain, Windy, Cold — no stacked combos in the default rotation | No combo conditions ship in the default rotation |
| WTH-3 | P1 | Rain: reduced grip; rain tires grip in wet but underperform in dry, slicks the reverse | Compound choice measurably changes race performance; puddles create line-choice skill moments |
| WTH-4 | P2 | Windy: directional wind force; aero parts counter it but add weight/drag | Forecast direction announced pre-race; gusts telegraphed visually (flags, particles) |
| WTH-5 | P2 | Cold: tires start cold and warm up; compound choice (soft grips cold but overheats, hard fast when warm, risky lap 1) | Tire temperature is legible via UI; first-lap cold grip reads as skill, not randomness |
| WTH-6 | P2 | Every condition has a build counter and a skill counter; all effects are visible (wet sheen, spray, wind visuals, temp UI) | Playtesters can name the active condition and what to build for it |

Design notes in `GAME_OVERVIEW.md`; not part of the MVP — candidate for the first P1/P2 systems (mostly parameter modulation on top of existing physics).

### 6.10 Steam Integration — P2 (playtest), P3 (launch)

| ID | Priority | Requirement | Acceptance criteria |
|---|---|---|---|
| STM-1 | P2 | Steamworks init, overlay-safe UI, app ID abstraction (dev vs prod) | Builds run under dev app ID; overlay works |
| STM-2 | P2 | Steam friends lobby + invite | Invites reach a running lobby |
| STM-3 | P3 | Achievements + stats + cloud saves | Verified end-to-end on a second machine |
| STM-4 | P3 | Rich presence, screenshots, Steam Input | Presence shows match state; screenshots capture gameplay |
| STM-5 | P3 | Release depots, default/beta/playtest branches, SteamPipe automation | Clean reproducible build upload |
| STM-6 | P3 | Store page assets complete (§5.4) | Page approved and public ≥ 90 days pre-launch |

---

## 7. MVP Requirements — **Confirmed** (from `MVP.md`)

**Goal: Build a basic car → Drive it → Cross a finish line.**

| # | Requirement | Status |
|---|---|---|
| 1 | Enter a simple test environment | **Confirmed** |
| 2 | Build a basic functional vehicle (chassis + wheels + engine) | **Confirmed** |
| 3 | Drive that vehicle | **Confirmed** |
| 4 | Reach and cross a finish line (finish detection) | **Confirmed** |
| 5 | Reset and try again (fast iteration) | **Confirmed** |

**MVP success criteria:** player can build a basic vehicle · the vehicle is drivable · building choices begin to influence behavior · the player completes a simple course · the build-and-drive loop repeats quickly · the experience is promising enough to justify expansion. **Polish is not required.**

**MVP development order (Proposed, from `MVP.md`):** basic driving → basic construction → validation → simple course → finish detection → reset/retry → minimal build-to-race flow.

**P0 engine tasks this implies (new):** Unity project scaffold, Git LFS + Unity `.gitignore`, input system baseline, rigidbody vehicle prototype.

---

## 8. Non-Goals

**Must not block the MVP (Confirmed, from `MVP.md`):** community map editor · multiple polished maps · large parts catalog · large cosmetics catalog · progression systems · shops · battle passes · ranked modes · tournaments · wagering · complex matchmaking · advanced destruction/damage · sophisticated backend · large content pipelines · extensive optimization · final visual polish.

**Deferred until core-loop validation (P1 playtest):** multiplayer networking (architecture must not *prevent* it — keep clean boundaries per `ARCHITECTURE.md`) · full progression · monetization design · Workshop.

---

## 9. Milestones & Roadmap

Milestones are gated on evidence, not dates (team to add dates based on capacity).

| Milestone | Entry gate | Exit criteria | Key deliverables |
|---|---|---|---|
| **M0 — Prototype (P0)** | Unity + repo scaffold | MVP success criteria met (§7) | Playable local build-to-race slice |
| **M1 — Core-loop depth (P1)** | M0 passed | 2–3 tradeoff courses + parts; "one more race" signal | Internal playtests, format decision |
| **M2 — Multiplayer proof (P2)** | M1 passed | 4–8 remote players, stable sessions | Netcode decision recorded; playtest build |
| **M3 — Steam playtest** | M2 passed | Store page live; playtest cohort feedback | Wishlist baseline; balance data |
| **M4 — Launch (P3)** | Playtest data supports it | §2.2 targets owned; Deck Verified pass | Steam release, marketing assets |
| **M5 — Post-launch (P4)** | M4 passed | Community signal | Workshop/map creator, cosmetics depth, monetization |

---

## 10. Risks & Mitigations

| Risk | Likelihood | Impact | Mitigation |
|---|---|---|---|
| Building system too complex for a 2-min pressure phase | High | Core loop fails | P1: test with 3-part minimum build; tutorial-in-first-match; validation feedback speed (BLD-5) |
| Physics feel unsatisfying or unreadable | Medium | Core loop fails | Arcade-first tuning from P0; playtest "feels good" gates (PHY-4) |
| 8–12 player physics sync too heavy/janky | Medium | Multiplayer promise fails | Start at 4–8 (MP-1); host-authoritative simulation; URP perf budget from P1 |
| Build-phase duration wrong (stress vs boredom) | Medium | Retention/feel issues | Explicit A/B in P1 playtests (§4.2) |
| Scope creep (map editor, modes, shops) | High | Never ships | Confirmed non-goals (§8); scope rule from `MVP.md` |
| Steam discoverability (crowded racing category) | Medium | Low launch sales | 90-day page lead, demo/Next Fest, playtest community, clip-friendly design (§4.4) |
| Refund-heavy launch from expectation mismatch | Medium | Store algorithm hit | Clear store copy/curated capsule; demo sets expectations; playtest feedback loop |
| 3-person team + AI sessions conflicting | Medium | Velocity loss | Existing `TEAM_WORKFLOW.md`/`TASKS.md` ownership model; small PRs; docs-first decisions |
| Team capacity/velocity unknown | High | Date risk | Milestones are evidence-gated (§9); MVP-first ordering |

---

## 11. Open Decisions (carried from `DECISIONS.md`, plus new)

| Decision | Current state | Blocks |
|---|---|---|
| Game engine/platform → **Unity** | **Confirmed (direction 2026-08-09)** — needs formal entry in `DECISIONS.md` | Architecture docs, repo scaffold |
| Target platform → **Steam** | **Confirmed (direction 2026-08-09)** — needs formal entry in `DECISIONS.md` | Store page, build pipeline |
| Build phase duration (~2 min proposed) | **Open Decision** | Match flow, course pacing |
| Player count (8–12 proposed) | **Open Decision** | Networking architecture |
| Camera approach | **Open Decision** | P1 driving feel |
| Building controls / attachment system | **Open Decision** | BLD-1/2 implementation |
| Vehicle validation rules | **Open Decision** | BLD-5 |
| Physics complexity / damage | **Open Decision** | PHY depth |
| Race format & checkpoint rules | **Open Decision** | RAC-3/4 |
| Networking implementation | **Open Decision** | P2 architecture |
| Monetization | **Open Decision** | Store page pricing, PRG-5 |
| Progression details | **Open Decision** | PRG set at P3 |

**Process:** when a decision closes, record it in `DECISIONS.md` and update affected docs (per the repo's decision process).

---

## 12. References

- `README.md` — project overview & core loop
- `docs/GAME_OVERVIEW.md` — concept, principles, intended match flow
- `docs/MVP.md` — confirmed MVP scope, non-goals, success criteria
- `docs/DECISIONS.md` — confirmed/proposed/open decisions
- `docs/ARCHITECTURE.md` — technical boundaries (to be expanded once Unity is recorded)
- `docs/TEAM_WORKFLOW.md`, `docs/TASKS.md`, `CODEX.md` — collaboration & AI-session rules
