# Work Board

Living checklist of everything in flight for **Made to Race** — check boxes off as
work completes, with evidence. Detailed milestone breakdown: [`TASKS.md`](TASKS.md).
Product vision & requirements: [`PRD.md`](PRD.md). Decisions: [`DECISIONS.md`](DECISIONS.md).

## How this board works

- `[x]` = done **and verified** (evidence noted — test counts, commits, PR links).
- `[ ]` = in flight or claimable. **Claim before starting**: put your name on the
  line (e.g. `- [ ] M0-7 build UI — *claimed: Alex*`). No-work-without-claim rule.
- Work moves **Done** only via merged PR with green verification (EditMode +
  PlayMode suites + pure-core checks where applicable).
- Merge order matters for stacked branches: **#6 → #7 → #8**.

---

## ✅ Done (merged to main)

- [x] **M0-1 — Unity project scaffold + test harness** — commit `3259a98`.
  EditMode **4/4** green against the real engine (`-runTests`, no `-quit`).
- [x] **Foundations** — PRs #1–#5 merged: PRD (requirements M0–M5), engine
  decision (Unity 6 LTS + Steam), scaffold, weather design (Proposed), TASKS
  breakdown. Unity **6000.0.81f1** installed (arm64) + Windows Mono module.

## 🔎 In review (PRs open — unmerged, stacked)

### M0-2 — Basic driving — [PR #6](https://github.com/JJL9/Made-to-race/pull/6) `feature/basic-driving`
- [x] VehicleController v2 + PlayerInputDriver (WASD/gamepad) + CameraFollow
- [x] PrototypeDrive scene generated headlessly
- [x] EditMode **4/4**, PlayMode **5/5**
- [ ] **Reviewed & merged by a brother** ← *needed: one approval + merge*

### M0-3 — Basic construction — [PR #7](https://github.com/JJL9/Made-to-race/pull/7) `feature/vehicle-builder`
- [x] VehicleBuild (pure build state) + BuildPhaseController (keys 1/2/3 = wheels/engine/reset)
- [x] Build consequences: engine gates power; race-ready = chassis + wheels + engine
- [x] PrototypeBuild scene generated headlessly
- [x] EditMode **13/13**, PlayMode **10/10**
- [ ] **Reviewed & merged by a brother** ← *needed: one approval + merge*

### M0-4 — Engineer-grade vehicle physics — [PR #8](https://github.com/JJL9/Made-to-race/pull/8) `feature/vehicle-physics`
- [x] Decision recorded **Confirmed** in DECISIONS.md (mechanic/engineer-grade, simcade)
- [x] VehiclePhysics pure core (power curve, drag/downforce ∝ v², traction, weight transfer) — dotnet **13/13**
- [x] WheelModel raycast suspension (spring + damper, weight transfer emerges)
- [x] VehicleController v3 friction circle — no speed cap, no yaw clamp; slide/oversteer emerge
- [x] PartSpecs (kart data: 15 kW, μ 1.0, Cd 0.7, 177 kg) drive the build (BLD-4: no wheels = no traction)
- [x] Input mapping extracted to pure `ComputeInput` (InputTestFixture corrupts Input System in batchmode)
- [x] EditMode **31/31**, PlayMode **9/9**, scenes regenerated
- [x] **Flip-on-spawn fix** — scene spawn height was below suspension rest (spring catapult); now spawns at 1.2, compression travel clamped
- [ ] **Reviewed & merged by a brother** ← *needed: one approval + merge*

## 🚧 In progress

- [ ] **Playtest feedback pass** — *claimed: user* — after the flip fix: does the
      kart feel right? (launch dig, top-end taper, braking, cornering grip,
      wheelspin). Open items become tasks below.

## 📋 Up next (claimable — pick one, put your name on it)

- [ ] **M0-5 — Finish line** (PRD R-6/R-7): trigger-based finish detection + lap/race time. *Ready.*
- [ ] **M0-6 — Race start** (PRD R-5): build-phase → countdown → GO. *Ready.*
- [ ] **M0-7 — Real build UI** (PRD BLD-3): part picker instead of keys 1/2/3
      (wheel/engine cards, drag or click-to-place). *Ready.*
- [ ] **M0-4.x — Rollover/crash behavior**: flipping the car is now possible —
      decide flop (reset to track) vs roll-back (physics). Design note needed
      before coding.
- [ ] **M0-4.x — Suspension/steering feel pass**: spring rate, damping, steering
      authority as real tuneable numbers (PartSpecs-driven), one test course.
- [ ] **WTH-1 — Wind condition** (Proposed, PR #4): aero drag modifier per race —
      natural fit with the drag/downforce model. *Design ready, not MVP-gated.*

## ❓ Needs a decision (brothers, weigh in)

- [ ] Chassis part variety: does the chassis come in mass/aero variants at MVP
      (BLD-4 weight-vs-fragility) or stay fixed until M1?
- [ ] Rollover: flop-and-reset vs physically rollable car (affects suspension/CoG tuning).
- [ ] When do we open the Steam page / start Steamworks integration? (PRD §6.10
      says post-MVP; revisit after M0-5.)
