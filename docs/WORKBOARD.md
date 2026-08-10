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

- [x] **Foundations** — PRs #1–#5 merged: PRD (requirements M0–M5), engine
  decision (Unity 6 LTS + Steam), scaffold, weather design (Proposed), TASKS
  breakdown. Unity **6000.0.81f1** installed (arm64) + Windows Mono module.
- [x] **M0-1 — Unity project scaffold + test harness** — commit `3259a98`.
  EditMode **4/4** green against the real engine (`-runTests`, no `-quit`).
- [x] **M0-2 — Basic driving** — PR #6 merged (squash `5efdd61`): VehicleController
  v2 + PlayerInputDriver (WASD/gamepad) + CameraFollow + PrototypeDrive scene.
  EditMode **4/4**, PlayMode **5/5**.
- [x] **M0-3 — Basic construction** — PR #7 merged (squash `cfe95d0`): VehicleBuild +
  BuildPhaseController (keys 1/2/3 = wheels/engine/reset) + PrototypeBuild scene.
  EditMode **13/13**, PlayMode **10/10**.
- [x] **M0-4 — Engineer-grade vehicle physics** — PR #8 merged (squash `b399e14`):
  pure physics core, raycast suspension, friction circle, PartSpecs-driven build,
  kart CoG, world-frame suspension. EditMode **31/31**, PlayMode **9/9**, playtest
  approved. **Main is now playable end-to-end: build car → drive it.** 🎉

## 🔎 In review (PRs open — unmerged, stacked)

*(none — all M0-2/M0-3/M0-4 PRs merged; next work lands via new PRs)*

## 🚧 In progress

- [x] **Playtest feedback pass** — *claimed: user* — verdict: **good**. Launch wheelie
      is catchable, top end tapers, braking grip-limited, slides controllable.
      Follow-ups (crash/rollover behavior, suspension feel pass) are queued below.

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
