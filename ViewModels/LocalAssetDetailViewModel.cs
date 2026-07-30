using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.IO;
using System.Reactive;
using System.Text.Json;
using System.Threading.Tasks;

namespace Panzerfaust.ViewModels
{
    internal class LocalAssetDetailViewModel : ReactiveObject
    {
        public string AssetId   { get; }
        public string FileName  { get; }
        public string Format    { get; }
        public string Resolution { get; }
        public string FilePath  { get; }
        public string SizeLabel { get; }

        private Bitmap? _thumbnail;
        public Bitmap? Thumbnail
        {
            get => _thumbnail;
            private set => this.RaiseAndSetIfChanged(ref _thumbnail, value);
        }

        private int _vertexCount;
        public int VertexCount
        {
            get => _vertexCount;
            private set => this.RaiseAndSetIfChanged(ref _vertexCount, value);
        }

        private int _indexCount;
        public int IndexCount
        {
            get => _indexCount;
            private set => this.RaiseAndSetIfChanged(ref _indexCount, value);
        }

        private int _triangleCount;
        public int TriangleCount
        {
            get => _triangleCount;
            private set => this.RaiseAndSetIfChanged(ref _triangleCount, value);
        }

        private bool _isLoadingStats;
        public bool IsLoadingStats
        {
            get => _isLoadingStats;
            private set => this.RaiseAndSetIfChanged(ref _isLoadingStats, value);
        }

        public ReactiveCommand<Unit, Unit> CloseCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowInFinderCommand { get; }

        public LocalAssetDetailViewModel(LocalAssetViewModel asset, Action closeHandler,
            Action<LocalAssetViewModel> showInFinderHandler)
        {
            AssetId    = asset.AssetId;
            FileName   = asset.FileName;
            Format     = asset.Format;
            Resolution = asset.Resolution;
            FilePath   = asset.FilePath;

            long bytes = new FileInfo(asset.FilePath).Length;
            SizeLabel = bytes >= 1_048_576
                ? $"{bytes / 1_048_576.0:F1} MB"
                : $"{bytes / 1024.0:F0} KB";

            CloseCommand       = ReactiveCommand.Create(closeHandler);
            ShowInFinderCommand = ReactiveCommand.Create(() => showInFinderHandler(asset));

            if (!string.IsNullOrEmpty(asset.ThumbnailPath))
                _ = LoadThumbnailAsync(asset.ThumbnailPath);

            _ = LoadMeshStatsAsync(asset.FilePath);
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

        private async Task LoadMeshStatsAsync(string gltfPath)
        {
            IsLoadingStats = true;
            try
            {
                var (verts, idx) = await Task.Run(() => ReadMeshCounts(gltfPath));
                VertexCount   = verts;
                IndexCount    = idx;
                TriangleCount = idx / 3;
            }
            catch { }
            finally { IsLoadingStats = false; }
        }

        private static (int verts, int indices) ReadMeshCounts(string gltfPath)
        {
            var json = File.ReadAllText(gltfPath);
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (!root.TryGetProperty("accessors", out var accessors)) return (0, 0);
            if (!root.TryGetProperty("meshes", out var meshes))       return (0, 0);

            int totalVerts = 0, totalIdx = 0;

            foreach (var mesh in meshes.EnumerateArray())
            {
                if (!mesh.TryGetProperty("primitives", out var prims)) continue;
                foreach (var prim in prims.EnumerateArray())
                {
                    // Vertex count from POSITION accessor
                    if (prim.TryGetProperty("attributes", out var attrs) &&
                        attrs.TryGetProperty("POSITION", out var posIdx))
                    {
                        var acc = accessors[posIdx.GetInt32()];
                        if (acc.TryGetProperty("count", out var c))
                            totalVerts += c.GetInt32();
                    }

                    // Index count from indices accessor
                    if (prim.TryGetProperty("indices", out var idxProp))
                    {
                        var acc = accessors[idxProp.GetInt32()];
                        if (acc.TryGetProperty("count", out var c))
                            totalIdx += c.GetInt32();
                    }
                }
            }

            // If no explicit indices, generate implied (one index per vertex)
            if (totalIdx == 0) totalIdx = totalVerts;
            return (totalVerts, totalIdx);
        }
    }
}
