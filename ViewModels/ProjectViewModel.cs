using Panzerfaust.Models;
using Panzerfaust.Service;
using ReactiveUI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Panzerfaust.ViewModels
{
    internal class ProjectViewModel : ViewModelBase
    {
        private readonly Project _project;
        private readonly IProjectService _projectService;
        private readonly IEngineService _engineService;
        private Interaction<string, bool>? _deleteProjectInteraction;
        private Interaction<EnginePickerViewModel, InstalledEngineViewModel?>? _enginePickerInteraction;
        private System.Collections.ObjectModel.ObservableCollection<InstalledEngineViewModel>? _installedEngines;

        private string _name;
        public string Name
        {
            get => _name;
            private set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public string Path => _project.Fullpath;
        public string UpdatedDate => _project.UpdateDate.ToShortDateString();
        public string CreatedDate => _project.CreationDate.ToShortDateString();
        public string ProjectSize => GetProjectSize();

        public string? ThumbnailPath
        {
            get
            {
                var thumb = System.IO.Path.Combine(_project.Fullpath, "thumbnail.png");
                return System.IO.File.Exists(thumb) ? thumb : null;
            }
        }

        private string? _preferredEngineVersion;
        public string? PreferredEngineVersion
        {
            get => _preferredEngineVersion;
            private set => this.RaiseAndSetIfChanged(ref _preferredEngineVersion, value);
        }

        private bool _isRenaming;
        public bool IsRenaming
        {
            get => _isRenaming;
            private set => this.RaiseAndSetIfChanged(ref _isRenaming, value);
        }

        private string _pendingName = string.Empty;
        public string PendingName
        {
            get => _pendingName;
            set => this.RaiseAndSetIfChanged(ref _pendingName, value);
        }

        public ReactiveCommand<Unit, Unit> OpenProjectCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteProjectCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowInfoCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearPreferredEngineCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenLocationCommand { get; }
        public ReactiveCommand<Unit, Unit> BeginRenameCommand { get; }
        public ReactiveCommand<Unit, Unit> ConfirmRenameCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelRenameCommand { get; }

        public ProjectViewModel(Project p, IProjectService projectService, IEngineService engineService,
            Interaction<string, bool>? deleteInteraction = null,
            Interaction<EnginePickerViewModel, InstalledEngineViewModel?>? enginePickerInteraction = null,
            System.Collections.ObjectModel.ObservableCollection<InstalledEngineViewModel>? installedEngines = null)
        {
            _project = p;
            _projectService = projectService;
            _engineService = engineService;
            _deleteProjectInteraction = deleteInteraction;
            _enginePickerInteraction = enginePickerInteraction;
            _installedEngines = installedEngines;
            _preferredEngineVersion = p.PreferredEngineVersion;
            _name = p.Name;
            OpenProjectCommand = ReactiveCommand.CreateFromTask(OnOpenProjectCommand);
            OpenProjectCommand.ThrownExceptions.Subscribe(ex =>
                MessageBus.Current.SendMessage<(string, string)>((Message.ToastErrorAction, $"Failed to open project: {ex.Message}")));
            DeleteProjectCommand = ReactiveCommand.CreateFromTask(OnDeleteProjectCommand);
            ShowInfoCommand = ReactiveCommand.Create(() =>
                MessageBus.Current.SendMessage<(string, ProjectViewModel)>((Message.ShowInfoAction, this)));
            ClearPreferredEngineCommand = ReactiveCommand.CreateFromTask(OnClearPreferredEngine);
            OpenLocationCommand = ReactiveCommand.Create(OnOpenLocation);
            BeginRenameCommand = ReactiveCommand.Create(() => { PendingName = _name; IsRenaming = true; });
            CancelRenameCommand = ReactiveCommand.Create(() => { IsRenaming = false; });

            var canConfirm = this.WhenAnyValue(x => x.PendingName)
                .Select(n => !string.IsNullOrWhiteSpace(n) && n != _name);
            ConfirmRenameCommand = ReactiveCommand.CreateFromTask(OnConfirmRename, canConfirm);
        }

        public void SetRemovalInteraction(Interaction<string, bool> interaction) => _deleteProjectInteraction = interaction;

        public void SetEnginePickerInteraction(
            Interaction<EnginePickerViewModel, InstalledEngineViewModel?> interaction,
            System.Collections.ObjectModel.ObservableCollection<InstalledEngineViewModel> engines)
        {
            _enginePickerInteraction = interaction;
            _installedEngines = engines;
        }

        private async Task OnDeleteProjectCommand()
        {
            if (_deleteProjectInteraction == null) { return; }

            var result = await _deleteProjectInteraction.Handle(_project.Name).ToTask();
            if (result)
            {
                await _projectService.DeleteAsync(_project);
                MessageBus.Current.SendMessage<(string, ProjectViewModel)>((Message.DeleteAction, this));
            }
        }

        private async Task OnOpenProjectCommand()
        {
            try
            {
                if (_enginePickerInteraction != null && _installedEngines != null)
                {
                    // Use the pinned engine directly if one was saved
                    var pinned = _installedEngines.FirstOrDefault(e => e.Version == _preferredEngineVersion);
                    if (pinned != null)
                    {
                        await _engineService.StartAsync(_project.Fullpath, pinned.BinaryPath).ConfigureAwait(false);
                        return;
                    }

                    var pickerVm = new EnginePickerViewModel(_project.Name, _installedEngines);
                    var chosen = await _enginePickerInteraction.Handle(pickerVm).ToTask();
                    if (chosen == null) return;

                    // Persist the choice as the preferred engine for next time
                    _project.PreferredEngineVersion = chosen.Version;
                    PreferredEngineVersion = chosen.Version;
                    await _projectService.SaveAsync(_project).ConfigureAwait(false);

                    await _engineService.StartAsync(_project.Fullpath, chosen.BinaryPath).ConfigureAwait(false);
                }
                else
                {
                    await _engineService.StartAsync(_project.Fullpath).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                MessageBus.Current.SendMessage<(string, string)>((Message.ToastErrorAction, $"Failed to open project: {ex.Message}"));
            }
        }

        private async Task OnConfirmRename()
        {
            var trimmed = PendingName.Trim();
            if (string.IsNullOrEmpty(trimmed)) return;
            try
            {
                await _projectService.RenameAsync(_project, trimmed).ConfigureAwait(false);
                Name = trimmed;
                IsRenaming = false;
            }
            catch (Exception ex)
            {
                MessageBus.Current.SendMessage<(string, string)>((Message.ToastErrorAction, $"Rename failed: {ex.Message}"));
            }
        }

        private async Task OnClearPreferredEngine()
        {
            _project.PreferredEngineVersion = null;
            PreferredEngineVersion = null;
            await _projectService.SaveAsync(_project).ConfigureAwait(false);
        }

        private void OnOpenLocation()
        {
            try
            {
                if (!Directory.Exists(_project.Fullpath)) return;
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start("explorer.exe", $"\"{_project.Fullpath}\"");
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    Process.Start("open", $"\"{_project.Fullpath}\"");
                else
                    Process.Start("xdg-open", $"\"{_project.Fullpath}\"");
            }
            catch (Exception ex)
            {
                MessageBus.Current.SendMessage<(string, string)>((Message.ToastErrorAction, $"Could not open location: {ex.Message}"));
            }
        }

        private string GetProjectSize()
        {
            try
            {
                if (!Directory.Exists(_project.Fullpath)) return "N/A";
                long bytes = 0;
                foreach (var f in new DirectoryInfo(_project.Fullpath).EnumerateFiles("*", SearchOption.AllDirectories))
                    bytes += f.Length;
                return bytes switch
                {
                    < 1024 => $"{bytes} B",
                    < 1024 * 1024 => $"{bytes / 1024.0:F1} KB",
                    < 1024 * 1024 * 1024 => $"{bytes / (1024.0 * 1024):F1} MB",
                    _ => $"{bytes / (1024.0 * 1024 * 1024):F1} GB"
                };
            }
            catch
            {
                return "N/A";
            }
        }
    }
}
