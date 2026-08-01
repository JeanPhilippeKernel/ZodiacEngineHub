using Panzerfaust.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Panzerfaust.Service
{
    public class EngineService : IEngineService
    {
        string _enginePath = string.Empty;
        string _workingDirectory = string.Empty;

        const string _launcherCLIAppName = "Obelisk";
        const string _configJsonFilename = "projectConfig.json";
        const string _projectFileCommandLineArgs = "--projectConfigFile";
        const string _launchEditorFlag = "--launchEditor";
        const string _launchEditorValue = "1";

        static string EngineExtension =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".exe" : string.Empty;

        public EngineService()
        {
            var appDir = Path.GetDirectoryName(AppContext.BaseDirectory) ?? Environment.CurrentDirectory;
            _enginePath = Path.Combine(appDir, $"{_launcherCLIAppName}{EngineExtension}");
            _workingDirectory = appDir;
        }

        public IEnumerable<InstalledEngine> ScanInstalledEngines(string installLocation)
        {
            if (!Directory.Exists(installLocation))
                yield break;

            var binaryName = $"{_launcherCLIAppName}{EngineExtension}";

            foreach (var versionDir in Directory.EnumerateDirectories(installLocation))
            {
                var binaryPath = Directory
                    .EnumerateFiles(versionDir, binaryName, SearchOption.AllDirectories)
                    .FirstOrDefault();

                if (binaryPath != null)
                    yield return new InstalledEngine
                    {
                        Version = Path.GetFileName(versionDir),
                        BinaryPath = binaryPath,
                        InstallPath = versionDir
                    };
            }
        }

        public Task StartAsync(string projectPath) => StartAsync(projectPath, _enginePath);

        public async Task StartAsync(string projectPath, string engineBinaryPath)
        {
            var configPath = Path.Combine(projectPath, _configJsonFilename);
            List<string> engineArgs = new() { _projectFileCommandLineArgs, configPath, _launchEditorFlag, _launchEditorValue };

            var workingDir = Path.GetDirectoryName(engineBinaryPath) ?? _workingDirectory;

            var processStartInfo = new ProcessStartInfo(engineBinaryPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir
            };

            // Extend the dynamic library search path so the engine finds its bundled dylibs.
            // Covers two layouts:
            //   - GitHub release zip: dylibs sit next to the binary (workingDir)
            //   - Local dev build:    dylibs are in a lib/ folder somewhere up the tree
            var searchDirs = new List<string> { workingDir };
            var dir = workingDir;
            for (var i = 0; i < 4; i++)
            {
                dir = Path.GetDirectoryName(dir);
                if (dir == null) break;
                var candidate = Path.Combine(dir, "lib");
                if (Directory.Exists(candidate))
                {
                    searchDirs.Add(candidate);
                    break;
                }
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                var existing = processStartInfo.Environment.TryGetValue("PATH", out var cur) ? cur : "";
                var extraPaths = string.Join(";", searchDirs);
                processStartInfo.Environment["PATH"] = string.IsNullOrEmpty(existing)
                    ? extraPaths : $"{extraPaths};{existing}";
            }
            else
            {
                var envVar = RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "DYLD_LIBRARY_PATH" : "LD_LIBRARY_PATH";
                var separator = ":";
                var existing = processStartInfo.Environment.TryGetValue(envVar, out var cur) ? cur : "";
                var extraPaths = string.Join(separator, searchDirs);
                processStartInfo.Environment[envVar] = string.IsNullOrEmpty(existing)
                    ? extraPaths : $"{extraPaths}{separator}{existing}";
            }
            foreach (var arg in engineArgs)
                processStartInfo.ArgumentList.Add(arg);

            if (!File.Exists(engineBinaryPath))
                throw new Exception($"Engine binary not found: {engineBinaryPath}");

            if (!File.Exists(configPath))
                throw new Exception($"Project config not found: {configPath}");

            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                // GitHub release zips extract without execute bits — fix all binaries/tools in the bin dir.
                // Do this before codesign so we don't invalidate an existing signature via chmod.
                var nonDataFiles = Directory.EnumerateFiles(workingDir)
                    .Where(f =>
                    {
                        var ext = System.IO.Path.GetExtension(f).ToLowerInvariant();
                        return ext is not (".dylib" or ".so" or ".json" or ".txt" or ".md" or ".xml" or ".plist" or ".ini" or ".png" or ".hdr");
                    });

                foreach (var exe in nonDataFiles)
                {
                    var mode = File.GetUnixFileMode(exe);
                    if (!mode.HasFlag(UnixFileMode.UserExecute))
                        File.SetUnixFileMode(exe,
                            mode | UnixFileMode.UserExecute | UnixFileMode.GroupExecute | UnixFileMode.OtherExecute);
                }
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // TEMPORARY: Ad-hoc re-sign every binary and dylib in the install tree.
                // GitHub release zips corrupt existing signatures; Gatekeeper SIGKILLs (137) anything invalid.
                // Remove this block once the Obelisk CI pipeline signs with a Developer ID and packages
                // with `ditto` instead of `zip`. See ZEngine/docs/macos-codesign-distribution.md.
                var installRoot = Path.GetDirectoryName(workingDir) ?? workingDir;
                var signable = Directory.EnumerateFiles(installRoot, "*", SearchOption.AllDirectories)
                    .Where(f =>
                    {
                        var ext = System.IO.Path.GetExtension(f).ToLowerInvariant();
                        return ext is ".dylib" or ".so" or ""
                            || (ext == string.Empty && File.GetUnixFileMode(f).HasFlag(UnixFileMode.UserExecute));
                    });

                foreach (var target in signable)
                {
                    using var cs = Process.Start(new ProcessStartInfo("codesign")
                    {
                        UseShellExecute = false,
                        RedirectStandardError = true,
                        ArgumentList = { "--force", "--sign", "-", target }
                    })!;
                    await Task.Run(() => cs.WaitForExit(10000));
                }
            }

            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;

            var engineProcess = Process.Start(processStartInfo)!;

            // Wait up to 3s — long enough to catch fast crashes, short enough not to block.
            var exited = await Task.Run(() => engineProcess.WaitForExit(3000));
            if (exited)
            {
                var stderr = await engineProcess.StandardError.ReadToEndAsync();
                var stdout = await engineProcess.StandardOutput.ReadToEndAsync();
                var output = string.IsNullOrWhiteSpace(stderr) ? stdout : stderr;
                throw new Exception($"Engine exited (code {engineProcess.ExitCode}){(string.IsNullOrWhiteSpace(output) ? "" : $": {output.Trim()}")}");
            }
        }
    }
}
