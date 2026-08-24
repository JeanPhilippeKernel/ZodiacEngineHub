# Release Process

Panzerfaust uses [Release Please](https://github.com/googleapis/release-please) and [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) to automate versioning and publishing. All releases — RC and stable — are driven from `develop`. No manual version bumping, no develop → main merge dance.

---

## Branches

| Branch | Purpose |
|---|---|
| `develop` | Default branch. All development, all releases. |
| `main` | Read-only mirror of the last stable release. Updated manually after each stable if desired. |

---

## Day-to-day development

Open PRs against `develop`. Use [Conventional Commits](https://www.conventionalcommits.org/en/v1.0.0/) in your PR title:

```
feat(scope): add something new        → minor bump
fix(scope): correct a bug             → patch bump
feat!: breaking change                → major bump
ci/docs/chore: housekeeping           → no bump
```

---

## Cutting a pre-release (RC)

The pre-release pipeline runs automatically on every push to `develop`. No action needed — if the push contains releasable commits, a new `vX.Y.Z-rc.N` tag is created and a GitHub Pre-release is published with build artifacts.

---

## Cutting a stable release

1. Review the open Release Please PR on `develop` (titled `chore: release X.Y.Z`).
2. Check the changelog looks correct.
3. Merge the PR.

Release Please then:
- Bumps `VERSION.txt` and `Panzerfaust.csproj` to `X.Y.Z`
- Updates `CHANGELOG.md`
- Creates tag `vX.Y.Z` on `develop`
- Publishes a GitHub Release with artifacts for Windows, macOS arm64, and Linux

That's it. No other steps required.

---

## Hotfix

For urgent fixes that cannot wait for the next RC cycle:

1. Branch off `develop`: `git checkout -b fix/<description> develop`
2. Apply the fix with a `fix:` commit.
3. Open a PR against `develop`.
4. Merge — Release Please updates the pending release PR with the fix.
5. Merge the Release Please PR to ship the patch release.

---

## Version bump rules

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
