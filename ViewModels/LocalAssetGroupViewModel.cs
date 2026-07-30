using Avalonia.Media.Imaging;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace Panzerfaust.ViewModels
{
    internal class LocalAssetGroupViewModel : ReactiveObject
    {
        public string AssetId { get; }
        public ObservableCollection<LocalAssetViewModel> Versions { get; }

        public Bitmap? Thumbnail => Versions[0].Thumbnail;
        public string AssetInitial => Versions[0].AssetInitial;
        public string PrimaryLabel => $"{Versions[0].Resolution} · {Versions[0].Format.ToUpperInvariant()}";
        public string VersionLabel => Versions.Count == 1 ? "1 version" : $"{Versions.Count} versions";

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set => this.RaiseAndSetIfChanged(ref _isExpanded, value);
        }

        public ReactiveCommand<Unit, Unit> ToggleExpandCommand { get; }
        public ReactiveCommand<Unit, Unit> OpenDetailCommand { get; }

        public LocalAssetGroupViewModel(string assetId,
            IEnumerable<LocalAssetViewModel> versions,
            Action<LocalAssetViewModel> onOpenDetail,
            Action<LocalAssetGroupViewModel> onToggleExpand)
        {
            AssetId = assetId;
            Versions = new ObservableCollection<LocalAssetViewModel>(
                versions.OrderByDescending(v =>
                    int.TryParse(v.Resolution.Replace("k", ""), out var n) ? n : 0));

            ToggleExpandCommand = ReactiveCommand.Create(() => onToggleExpand(this));
            OpenDetailCommand = ReactiveCommand.Create(() => onOpenDetail(Versions[0]));

            Versions[0].WhenAnyValue(v => v.Thumbnail)
                .Subscribe(_ => this.RaisePropertyChanged(nameof(Thumbnail)));
        }
    }
}
