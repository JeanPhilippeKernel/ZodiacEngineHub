# Project configuration

Panzerfaust writes `projectConfig.json` when creating a project. This is the
project configuration passed to Obelisk; engine documentation and issue reports
may also call this file `project.json`.

New projects include these defaults alongside their asset directories and scenes:

```json
{
  "skyDefaults": {
    "mode": "atmosphere",
    "environmentIntensity": 1,
    "environmentTint": [1, 1, 1, 1],
    "environmentYawRadians": 0
  },
  "rendering": {
    "environment_lighting_quality": "standard",
    "environment_lighting_budget_mb": 384
  }
}
```

## Sky defaults

`skyDefaults` supplies authoring defaults for newly created or reset scenes.
Saved scenes retain their own `SkyConfig`. Supported modes are `atmosphere`,
`hdri`, and `skySphere`. The generated atmosphere uses the engine's default
atmosphere settings.

Environment intensity is a nonnegative multiplier. Tint contains four
nonnegative RGBA components; white preserves the original colors. Yaw is a
rotation in radians. The generated values match the engine's validated defaults.

`environmentMap` is optional and omitted by default. When configuring an HDRI,
set it to the imported environment asset's UUID string, rather than a file path.
Generated IBL cache paths, renderer handles, and viewport state do not belong in
this section.

## Environment lighting

`rendering.environment_lighting_quality` controls environment-lighting bake
quality. The accepted tiers are:

| Tier | Intended use |
|---|---|
| `low` | Lower-cost previews or constrained hardware; smaller lighting maps and fewer samples. |
| `standard` | Default balance between lighting quality and bake cost. |
| `high` | Higher-quality lighting with larger maps and more samples. |

`rendering.environment_lighting_budget_mb` is a positive integer number of MiB.
The default is `384`. It caps persistent environment textures, independently of
the quality tier; it is neither a CPU memory budget nor a total GPU memory limit.

Existing projects require no migration. Missing or invalid lighting settings
fall back to `standard` and `384` in the engine. The editor also accepts the
legacy `sky` section. Panzerfaust creates only new project configurations and
does not rewrite existing configurations to add these defaults. Renaming a
project preserves its other configuration properties, including rendering
settings and legacy sky data.
