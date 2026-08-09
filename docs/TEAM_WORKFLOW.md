# Team Workflow

## Purpose

Made to Race is developed by three developers who may work in parallel.

This workflow is intended to:

- reduce merge conflicts
- avoid duplicated work
- keep `main` stable and playable
- make ownership of active work clear
- keep pull requests small and reviewable
- allow ChatGPT/Codex sessions to work safely with the team

GitHub is the source of truth for code, accepted documentation, and current project work.

## Branch Strategy

Use:

- `main` — stable and preferably playable
- `feature/...` — new features
- `fix/...` — bug fixes
- `docs/...` — documentation changes

Examples:

```text
feature/basic-driving
feature/vehicle-builder
fix/wheel-collision
docs/mvp-update
