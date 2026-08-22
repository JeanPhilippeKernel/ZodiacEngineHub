# Changelog

## [1.5.0](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/compare/v1.4.0...v1.5.0) (2026-08-22)


### Features

* assets page redesign, local asset grouping, project card modernization ([#16](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/16)) ([ae27d32](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/ae27d32a9a54cc50b2b23e8d1d1ca897c7b93191))
* configure VS Code for cross-platform debugging and fix binding errors ([#15](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/15)) ([e08d69c](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/e08d69cd2efd6ce89e680940557a57038cf9624b))
* engine UX improvements and Linux startup fixes ([#32](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/32)) ([d41dae2](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/d41dae22b8802e197d2f8f85bc019844f0d4a91b))
* **panzerfaust:** add search support allowing user to find projet by name ([#399](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/399)) ([#447](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/447)) ([69822bc](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/69822bcd05a78fdef78cfae0db3fc9aad7b46814))
* **panzerfaust:** build launcher UI with engine management, assets, and project workflow ([#554](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/554)) ([739b6af](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/739b6aff41ff533884e8a89c942978070fe062f8))
* **project:** remove SceneData directory from project layout ([#48](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/48)) ([7e816f8](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/7e816f884a53f5cfb2f7be4528525e58ffba51b8))
* **project:** standardize asset directories under Assets/ and add sky config ([#46](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/46)) ([90c8faf](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/90c8faf5a337844b5a68433b29800714e89348a3))
* RC build indicator with orange bloom border and version tag ([#28](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/28)) ([ec34bcb](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/ec34bcb230bec714bfd3317965c2b2210bf92186))
* show app release version in status bar ([#14](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/14)) ([5315617](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/5315617f48eee0656da496502f1541c15273e126))
* **ui:** add ZodiacEngineHub app icon ([#49](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/49)) ([52a0bea](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/52a0bea6177272cede9390baade5a4c2b640602f))


### Bug Fixes

* bump version to trigger RC build with orange border indicator ([#27](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/27)) ([7fada50](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/7fada506d65cdbde1700ec2557ddc520c2da4dfc))
* pin Tmds.DBus.Protocol to 0.20.0 to fix Linux startup crash ([#31](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/31)) ([cf79d5e](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/cf79d5e5698e505dc2b217a3a98697cb73e43bfc))
* restore Windows caption buttons and fix app version display ([#25](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/25)) ([7846204](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/784620441f95deb0f7103d69838fe3d86d794582))
* surface engine crash errors and resolve stale rpath for local builds ([#30](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/30)) ([2d9c5db](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/2d9c5dba7c511352a42ff9e489c05bf09de646ee))


### Documentation

* add README with features, downloads, system requirements and build instructions ([#12](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/12)) ([9ca1e05](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/9ca1e056d70d2b753ae070338c681b1f93976f0a))
* **roadmap:** add roadmap with planned features and current implementation status ([#24](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/24)) ([d4638a2](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/d4638a2db75d086abd7f34bdd6ae5a31c39abce7))
* update README with current screenshots ([#17](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/17)) ([f3b3555](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/f3b3555c4e0abc5dba71b3ab00e64c3a6f7adea5))


### Build System

* remove _WIN32/__MACOS__ defines, fix OutputType to Exe ([be9def5](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/be9def5c8e7b63292c84296e9eff7a021f685f0e))
* restore OutputType to WinExe ([2790a13](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/2790a13835d6816367bf0a71be3e87910b2de232))


### CI/CD

* add changelog generation script ([c8ca195](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/c8ca1954df450e5cc5532ad70950228620a799c0))
* add full CI/CD pipeline for all platforms ([edd6e35](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/edd6e358176212a0e4fc60839b4b6533410a9bb9))
* configure release-please and versioning for v1.0.0 ([da46c28](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/da46c2875d3cb43e7d10c9c369f5864397b0f4f1))
* drop macOS x64 build from all workflows ([#44](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/44)) ([eaf213c](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/eaf213c27f9a240e2bc1b5e58cc096576ca286a5))
* enable single-file publish across all platform build jobs ([#13](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/13)) ([f92afae](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/f92afae642d39d54d87ecf7ff16c86a0858cc3b4))
* ignore legacy non-conventional commits in commitlint ([#40](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/40)) ([60340d4](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/60340d4c97d3ff4c1a6aca063ee58e3dc95058c9))
* restore commitlint check for all PRs ([#39](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/39)) ([38aada5](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/38aada553b8546514858e4139fcf02ee7ebc37a7))
* rewrite pipelines for dotnet, remove C++ engine workflows ([0f97bfb](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/0f97bfb3e0d143fb20e8ed88884e152ccaee67b8))
* set target-branch to main in release-please config ([#53](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/53)) ([028b27c](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/028b27c7b7d8995e8390ebb744237d039b858f30))
* skip build for non-source file changes ([#45](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/45)) ([bb35a0e](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/bb35a0e9cea4e11f8ebe7b78669949784f404bb6))
* sync dotnet pipelines, configs and scripts from develop ([6c78b92](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/6c78b923f9aeb3815689407a3f401905d3b4455b))

## [1.4.0](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/compare/v1.3.0...v1.4.0) (2026-08-03)


### Features

* engine UX improvements and Linux startup fixes ([#32](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/32)) ([d41dae2](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/d41dae22b8802e197d2f8f85bc019844f0d4a91b))
* RC build indicator with orange bloom border and version tag ([#28](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/28)) ([ec34bcb](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/ec34bcb230bec714bfd3317965c2b2210bf92186))


### Bug Fixes

* bump version to trigger RC build with orange border indicator ([#27](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/27)) ([7fada50](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/7fada506d65cdbde1700ec2557ddc520c2da4dfc))
* pin Tmds.DBus.Protocol to 0.20.0 to fix Linux startup crash ([#31](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/31)) ([cf79d5e](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/cf79d5e5698e505dc2b217a3a98697cb73e43bfc))
* restore Windows caption buttons and fix app version display ([#25](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/25)) ([7846204](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/784620441f95deb0f7103d69838fe3d86d794582))
* surface engine crash errors and resolve stale rpath for local builds ([#30](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/30)) ([2d9c5db](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/2d9c5dba7c511352a42ff9e489c05bf09de646ee))


### Documentation

* **roadmap:** add roadmap with planned features and current implementation status ([#24](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/24)) ([d4638a2](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/d4638a2db75d086abd7f34bdd6ae5a31c39abce7))


### CI/CD

* ignore legacy non-conventional commits in commitlint ([#40](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/40)) ([60340d4](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/60340d4c97d3ff4c1a6aca063ee58e3dc95058c9))
* restore commitlint check for all PRs ([#39](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/issues/39)) ([38aada5](https://github.com/JeanPhilippeKernel/ZodiacEngineHub/commit/38aada553b8546514858e4139fcf02ee7ebc37a7))


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
