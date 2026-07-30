using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Panzerfaust.ViewModels
{
    internal class LocalAssetViewModel : ReactiveObject
    {
        public string AssetId { get; }
        public string FileName { get; }
        public string Resolution { get; }
        public string Format { get; }
        public string FilePath { get; }
        public long FileSizeBytes { get; }

        public string Label => $"{AssetId} · {Resolution} {Format.ToUpperInvariant()}";
        public string AssetInitial => AssetId.Length > 0 ? AssetId[0].ToString().ToUpperInvariant() : "?";

        // Raw path — kept for LocalAssetDetailViewModel which also needs it
        public string? ThumbnailPath { get; }

        private Bitmap? _thumbnail;
        public Bitmap? Thumbnail
        {
            get => _thumbnail;
            private set => this.RaiseAndSetIfChanged(ref _thumbnail, value);
        }

        public static string ShowInFilesLabel =>
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "Show in Explorer" :
            RuntimeInformation.IsOSPlatform(OSPlatform.OSX)     ? "Show in Finder" :
                                                                   "Show in Files";
        public string SizeLabel => FileSizeBytes >= 1_048_576
            ? $"{FileSizeBytes / 1_048_576.0:F1} MB"
            : $"{FileSizeBytes / 1024.0:F0} KB";

        public ReactiveCommand<Unit, Unit> ShowInFinderCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenDetailCommand { get; }

        public LocalAssetViewModel(string assetId, string resolution, string filePath,
            Action<LocalAssetViewModel> onShowInFinder,
            Func<LocalAssetViewModel, System.Threading.Tasks.Task> onDelete,
            Action<LocalAssetViewModel>? onOpenDetail = null)
        {
            AssetId = assetId;
            Resolution = resolution;
            FilePath = filePath;
            FileName = Path.GetFileName(filePath);
            Format = Path.GetExtension(filePath).TrimStart('.').ToLowerInvariant();
            FileSizeBytes = File.Exists(filePath) ? new FileInfo(filePath).Length : 0;

            // Find diffuse texture: prefer *_diff_* then fall back to any JPG in same dir
            var dir = Path.GetDirectoryName(filePath) ?? string.Empty;
            var diffFiles = Directory.GetFiles(dir, "*_diff_*");
            ThumbnailPath = diffFiles.Length > 0
                ? diffFiles[0]
                : System.Linq.Enumerable.FirstOrDefault(Directory.GetFiles(dir, "*.jpg"))
                  ?? System.Linq.Enumerable.FirstOrDefault(Directory.GetFiles(dir, "*.jpeg"));

            ShowInFinderCommand = ReactiveCommand.Create(() => onShowInFinder(this));
            DeleteCommand       = ReactiveCommand.CreateFromTask(() => onDelete(this));
            OpenDetailCommand   = ReactiveCommand.Create(() => onOpenDetail?.Invoke(this));

            if (ThumbnailPath != null)
                _ = LoadThumbnailAsync(ThumbnailPath);
        }

        private async Task LoadThumbnailAsync(string path)
        {
            try
            {
                var bytes = await Task.Run(() => File.ReadAllBytes(path));
                using var ms = new MemoryStream(bytes);
                Thumbnail = new Bitmap(ms);
            }
            catch { }
        }
    }
}
