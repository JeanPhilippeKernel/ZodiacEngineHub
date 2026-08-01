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
            var engineRoot = Path.GetDirectoryName(workingDir) ?? workingDir;
            var libDir = Path.Combine(engineRoot, "lib");

            var processStartInfo = new ProcessStartInfo(engineBinaryPath)
            {
                UseShellExecute = false,
                WorkingDirectory = workingDir
            };

            // Extend the dynamic library search path so the engine finds its bundled dylibs
            // even when the build rpath is stale (common with local dev builds).
            var existing = processStartInfo.Environment.TryGetValue("DYLD_LIBRARY_PATH", out var cur) ? cur : "";
            var extraPaths = string.Join(":", new[] { libDir, workingDir }.Where(Directory.Exists));
            processStartInfo.Environment["DYLD_LIBRARY_PATH"] = string.IsNullOrEmpty(existing)
                ? extraPaths
                : $"{extraPaths}:{existing}";
            foreach (var arg in engineArgs)
                processStartInfo.ArgumentList.Add(arg);

            if (!File.Exists(engineBinaryPath))
                throw new Exception($"Engine binary not found: {engineBinaryPath}");

            if (!File.Exists(configPath))
                throw new Exception($"Project config not found: {configPath}");

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
