# Release Process

Panzerfaust uses two branches and [Release Please](https://github.com/googleapis/release-please) to automate versioning and publishing.

| Branch | Track | Example tag |
|---|---|---|
| `develop` | Pre-release (RC) | `v1.2.0-rc.1` |
| `main` | Stable | `v1.2.0` |

All automation is driven by [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/). No manual version bumping.

---

## Day-to-day development

Contributors open PRs against `develop`. Once merged, Release Please on `develop` automatically opens or updates a **Pre-release PR** that accumulates the pending version bump and changelog entries.

---

## Cutting a pre-release (RC)

1. Review the open Release Please PR on `develop` (titled `chore: release X.Y.Z-rc.N`).
2. Check the changelog entries look correct.
3. Merge the PR.

Release Please then:
- Bumps `VERSION.txt` to e.g. `1.2.0-rc.1`
- Creates tag `v1.2.0-rc.1`
- Publishes a GitHub Pre-release with build artifacts for all platforms

Subsequent merges to `develop` before a stable release increment the counter: `rc.1` → `rc.2` → …

---

## Promoting to stable

Once `develop` is ready to ship:

1. Open a PR from `develop` → `main`.
2. Get it reviewed and merge it.
3. Release Please on `main` opens a **Stable Release PR** (titled `chore: release X.Y.Z`).
4. Review and merge that PR.

Release Please then:
- Bumps `VERSION.txt` to e.g. `1.2.0`
- Creates tag `v1.2.0`
- Publishes a GitHub Release (stable) with build artifacts

---

## Syncing `develop` after a stable release

After the stable Release PR merges to `main`, `develop` is behind by the version bump commit. Sync it:

```sh
git checkout develop
git fetch origin
git merge origin/main
git push origin develop
```

This keeps `VERSION.txt` and `CHANGELOG.md` consistent on both branches.

---

## Hotfix on stable

For urgent fixes that cannot wait for the next RC cycle:

1. Branch off `main`: `git checkout -b fix/<description> main`
2. Apply the fix with a `fix:` commit.
3. Open a PR against `main` directly.
4. Merge — Release Please opens a patch release PR (`v1.2.1`).
5. Merge the release PR.
6. Back-port to `develop`:

```sh
git checkout develop
git cherry-pick <fix-commit-sha>
git push origin develop
```

---

## Version bump rules

The version bump is determined automatically from commit types since the last release:

| Commit type | Bump |
|---|---|
| `feat` | minor |
| `fix`, `perf`, `refactor`, `revert` | patch |
| `feat!` or `BREAKING CHANGE:` footer | major |
| `docs`, `style`, `test`, `build`, `ci`, `chore` | none |

---

## Permissions required

The following must be enabled in **Settings → Actions → General** for Release Please to work:

- Workflow permissions: **Read and write**
- **Allow GitHub Actions to create and approve pull requests**: enabled
