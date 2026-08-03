using Panzerfaust.Models;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Panzerfaust.ViewModels
{
    internal class InstalledEngineViewModel : ReactiveObject
    {
        private static readonly Regex TagPattern = new(@"^v\d+\.\d+", RegexOptions.Compiled);
        private static readonly Regex RcPattern  = new(@"-rc\.", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        private readonly InstalledEngine _engine;

        public string Version => _engine.Version;
        public string BinaryPath => _engine.BinaryPath;
        public string InstallPath => _engine.InstallPath;

        // Only tag-versioned folders (v0.x.x, v1.2.3-rc.1, …) can be uninstalled
        public bool CanUninstall => TagPattern.IsMatch(_engine.Version);
        public bool IsRc => RcPattern.IsMatch(_engine.Version);

        public ReactiveCommand<Unit, Unit> UninstallCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenLocationCommand { get; }

        public InstalledEngineViewModel(InstalledEngine engine, Func<InstalledEngineViewModel, System.Threading.Tasks.Task> uninstallHandler)
        {
            _engine = engine;
            UninstallCommand = ReactiveCommand.CreateFromTask(() => uninstallHandler(this));
            OpenLocationCommand = ReactiveCommand.Create(OpenLocation);
        }

        private void OpenLocation()
        {
            try
            {
                var path = Directory.Exists(_engine.InstallPath)
                    ? _engine.InstallPath
                    : Path.GetDirectoryName(_engine.InstallPath) ?? _engine.InstallPath;

                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start("explorer.exe", path);
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    Process.Start("open", path);
                else
                    Process.Start("xdg-open", path);
            }
            catch { }
        }
    }
}
