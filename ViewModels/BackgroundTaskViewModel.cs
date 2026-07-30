using ReactiveUI;
using System;
using System.Diagnostics;
using System.Reactive;

namespace Panzerfaust.ViewModels
{
    internal enum BackgroundTaskStatus { Running, Succeeded, Failed }

    internal class BackgroundTaskViewModel : ReactiveObject
    {
        private BackgroundTaskStatus _status = BackgroundTaskStatus.Running;
        public BackgroundTaskStatus Status
        {
            get => _status;
            set
            {
                this.RaiseAndSetIfChanged(ref _status, value);
                this.RaisePropertyChanged(nameof(IsRunning));
                this.RaisePropertyChanged(nameof(IsSucceeded));
                this.RaisePropertyChanged(nameof(IsFailed));
                this.RaisePropertyChanged(nameof(StatusGlyph));
                this.RaisePropertyChanged(nameof(StatusColor));
            }
        }

        private string _label = string.Empty;
        public string Label
        {
            get => _label;
            set => this.RaiseAndSetIfChanged(ref _label, value);
        }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
        }

        private string _duration = string.Empty;
        public string Duration
        {
            get => _duration;
            private set => this.RaiseAndSetIfChanged(ref _duration, value);
        }

        public bool IsRunning => _status == BackgroundTaskStatus.Running;
        public bool IsSucceeded => _status == BackgroundTaskStatus.Succeeded;
        public bool IsFailed => _status == BackgroundTaskStatus.Failed;

        public string StatusGlyph => _status switch
        {
            BackgroundTaskStatus.Running => "⟳",
            BackgroundTaskStatus.Succeeded => "✓",
            BackgroundTaskStatus.Failed => "✕",
            _ => ""
        };

        public string StatusColor => _status switch
        {
            BackgroundTaskStatus.Succeeded => "#3ECF8E",
            BackgroundTaskStatus.Failed    => "#C42B1C",
            _                              => "#888888"
        };

        public ReactiveCommand<Unit, Unit> RetryCommand { get; }

        private readonly Func<Task> _work;

        public BackgroundTaskViewModel(string label, Func<Task> work)
        {
            _label = label;
            _work = work;
            RetryCommand = ReactiveCommand.CreateFromTask(RunAsync);
        }

        public Task RunAsync() => RunInternalAsync();

        private async Task RunInternalAsync()
        {
            Status = BackgroundTaskStatus.Running;
            ErrorMessage = string.Empty;
            Duration = string.Empty;
            var sw = Stopwatch.StartNew();
            try
            {
                await _work();
                sw.Stop();
                Duration = FormatDuration(sw.Elapsed);
                Status = BackgroundTaskStatus.Succeeded;
            }
            catch (Exception ex)
            {
                sw.Stop();
                Duration = FormatDuration(sw.Elapsed);
                ErrorMessage = ex.Message;
                Status = BackgroundTaskStatus.Failed;
            }
        }

        private static string FormatDuration(TimeSpan t) =>
            t.TotalSeconds < 1 ? $"{t.Milliseconds}ms"
            : t.TotalMinutes < 1 ? $"{t.TotalSeconds:F1}s"
            : $"{(int)t.TotalMinutes}m {t.Seconds}s";
    }
}
