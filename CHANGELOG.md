# Changelog

## [1.3.0](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/compare/v1.2.0...v1.3.0) (2026-07-30)


### Features

* assets page redesign, local asset grouping, project card modernization ([#16](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/16)) ([ae27d32](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/ae27d32a9a54cc50b2b23e8d1d1ca897c7b93191))
* configure VS Code for cross-platform debugging and fix binding errors ([#15](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/15)) ([e08d69c](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/e08d69cd2efd6ce89e680940557a57038cf9624b))
* show app release version in status bar ([#14](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/14)) ([5315617](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/5315617f48eee0656da496502f1541c15273e126))


### Documentation

* add README with features, downloads, system requirements and build instructions ([#12](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/12)) ([9ca1e05](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/9ca1e056d70d2b753ae070338c681b1f93976f0a))
* update README with current screenshots ([#17](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/17)) ([f3b3555](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/f3b3555c4e0abc5dba71b3ab00e64c3a6f7adea5))


### CI/CD

* enable single-file publish across all platform build jobs ([#13](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/13)) ([f92afae](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/f92afae642d39d54d87ecf7ff16c86a0858cc3b4))

## [1.2.0](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/compare/v1.1.0...v1.2.0) (2026-07-29)


### Features

* **panzerfaust:** add search support allowing user to find projet by name ([#399](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/399)) ([#447](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/447)) ([69822bc](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/69822bcd05a78fdef78cfae0db3fc9aad7b46814))
* **panzerfaust:** build launcher UI with engine management, assets, and project workflow ([#554](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/554)) ([739b6af](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/739b6aff41ff533884e8a89c942978070fe062f8))


### Build System

* remove _WIN32/__MACOS__ defines, fix OutputType to Exe ([be9def5](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/be9def5c8e7b63292c84296e9eff7a021f685f0e))
* restore OutputType to WinExe ([2790a13](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/2790a13835d6816367bf0a71be3e87910b2de232))


### CI/CD

* add changelog generation script ([c8ca195](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/c8ca1954df450e5cc5532ad70950228620a799c0))
* add full CI/CD pipeline for all platforms ([edd6e35](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/edd6e358176212a0e4fc60839b4b6533410a9bb9))
* configure release-please and versioning for v1.0.0 ([da46c28](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/da46c2875d3cb43e7d10c9c369f5864397b0f4f1))
* rewrite pipelines for dotnet, remove C++ engine workflows ([0f97bfb](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/0f97bfb3e0d143fb20e8ed88884e152ccaee67b8))
* sync dotnet pipelines, configs and scripts from develop ([6c78b92](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/6c78b923f9aeb3815689407a3f401905d3b4455b))

## [1.1.0](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/compare/v1.0.0...v1.1.0) (2026-07-29)


### Features

* **panzerfaust:** add search support allowing user to find projet by name ([#399](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/399)) ([#447](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/447)) ([69822bc](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/69822bcd05a78fdef78cfae0db3fc9aad7b46814))
* **panzerfaust:** build launcher UI with engine management, assets, and project workflow ([#554](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/554)) ([739b6af](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/739b6aff41ff533884e8a89c942978070fe062f8))


### Build System

* remove _WIN32/__MACOS__ defines, fix OutputType to Exe ([79f2f73](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/79f2f7304e4b9a0ababde86b8264b8b3a0a52e23))
* restore OutputType to WinExe ([d49d53f](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/d49d53ff6bd3359a91f6fe94cdfcc24ff56dda7f))


### CI/CD

* add changelog generation script ([fb33c4e](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/fb33c4e043c7d5ad09fd0c843f99ea79d5fc2705))
* add full CI/CD pipeline for all platforms ([4eda1d3](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/4eda1d3506ec0834fd8ec76cfc1c7321116582fb))
* configure release-please and versioning for v1.0.0 ([205b70b](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/205b70bddcdda7401646784b37d48d4c34d40b01))
* rewrite pipelines for dotnet, remove C++ engine workflows ([91cc309](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/91cc309e0add5a1f10cae026e39eadb2ded72281))
* sync dotnet pipelines, configs and scripts from develop ([4f3d5f3](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/4f3d5f3c221c6efdc9cbc9c1ff57146561d8e2cd))
