
# Contributing to Panzerfaust

When contributing to this repository, please first discuss the change you wish to make via issue.

## Branch target

**All pull requests must target the `develop` branch.** Direct PRs to `main` are not accepted — `main` is updated only through the release process.

## Rules

**Panzerfaust** is built upon 2 rules:

1. You cannot add code that will slow down the application startup or engine launch
2. You cannot add code that will make things complex to use

## Commit Message Convention

Panzerfaust uses [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/). Every commit message is linted on pull requests and drives automatic versioning — the type you choose determines how the version number is bumped.

### Format

```
<type>(<scope>): <subject>

[optional body]

[optional footer(s)]
```

- **type** — required, lowercase (see table below)
- **scope** — optional, lowercase, names the subsystem affected (e.g. `rendering`, `vulkan`, `camera`, `memory`, `ci`)
- **subject** — required, imperative mood, no trailing period, max 100 characters total for the header
- **body** — free prose; wrap at 72 characters; separate from subject with a blank line
- **footer** — key: value pairs; `BREAKING CHANGE: <description>` triggers a major version bump

### Types and version impact

| Type | When to use | Version bump |
|---|---|---|
| `feat` | A new feature visible to users or engine consumers | minor (`0.3.0` → `0.4.0`) |
| `fix` | A bug fix | patch (`0.3.0` → `0.3.1`) |
| `perf` | A performance improvement with no API change | patch |
| `refactor` | Code restructuring with no behaviour or API change | patch |
| `docs` | Documentation only | none |
| `style` | Formatting, whitespace — no logic change | none |
| `test` | Adding or correcting tests | none |
| `build` | Build system or external dependency changes | none |
| `ci` | CI/CD pipeline changes | none |
| `chore` | Maintenance tasks (e.g. release bumps) | none |
| `revert` | Reverts a previous commit | patch |

A `!` suffix on any type (e.g. `feat!:`) or a `BREAKING CHANGE:` footer triggers a **major** bump.

### Examples

```
feat(rendering): add indirect draw support for mesh batches
```

```
fix(vulkan): correct semaphore leak on swapchain recreation
```

```
perf(memory): replace per-frame heap alloc with arena in render loop
```

```
feat!: remove legacy OpenGL backend

BREAKING CHANGE: the OpenGL renderer has been removed. Vulkan is now
the only supported backend. Update your application startup code to
remove any OpenGL-specific initialisation.
```

```
refactor(camera): extract projection logic into CameraUtils

No behaviour change. Simplifies FlyCamera and OrbitCamera by sharing
the common frustum calculation.
```

### What happens if a commit message is wrong

A CI check (`commitlint`) runs on every pull request and will block the build if any commit in the PR branch does not follow the format. Fix the message with `git rebase -i` before requesting review.

## Local Setup

Run the hook installer once after cloning:

```sh
sh Scripts/install-hooks.sh
```

This installs a `commit-msg` hook that validates conventional commits format locally, catching violations before they hit CI.

## Release Process

ZEngine has two release tracks, both fully automated from commit messages.

### Stable releases (`main`)

Merges into `main` are picked up by Release Please, which opens a **Release PR** accumulating all changes since the last stable tag. When that PR is merged:

- `VERSION.txt` is bumped (`0.3.0` → `0.3.1` / `0.4.0` / `1.0.0`) based on commit types
- A tag `v0.3.1` is created
- A GitHub Release is published with build artifacts for all platforms

### Pre-releases (`develop`)

Pushes to `develop` follow the same process but produce rc versions:

- Release Please opens a **Pre-release PR** on `develop`
- When merged: `VERSION.txt` is set to e.g. `0.4.0-rc.1`, tag `v0.4.0-rc.1` is created
- A GitHub Pre-release is published (marked as pre-release in the GitHub UI)
- Each subsequent batch of commits increments the rc counter: `rc.1` → `rc.2` → …

### Promoting a pre-release to stable

Once `develop` is stable enough to ship:

1. Open a PR from `develop` → `main`
2. Merge it — Release Please on `main` sees all the accumulated `feat:`/`fix:` commits and opens a stable Release PR
3. Merge the Release PR → `v0.4.0` stable is tagged and published

## Pull Request Process

1. Make sure your modification is covered by the rules above and discussed in an issue first.

2. Update the README.md with details of changes to the interface, including new environment
   variables, exposed ports, useful file locations and container parameters.

3. You may merge the Pull Request once you have the sign-off of two other developers, or if you
   do not have permission to do that, you may request the second reviewer to merge it for you.

