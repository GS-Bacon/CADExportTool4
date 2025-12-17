using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using CADExportTool.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace CADExportTool.WPF.ViewModels;

/// <summary>
/// メインビューモデル
/// </summary>
public partial class MainViewModel : ObservableObject, IDropTarget
{
    private readonly IExportService _exportService;
    private CancellationTokenSource? _cancellationTokenSource;

    public MainViewModel(IExportService exportService)
    {
        _exportService = exportService;
    }

    #region Properties

    /// <summary>ファイルリスト</summary>
    [ObservableProperty]
    private ObservableCollection<CadFileItem> _fileList = [];

    /// <summary>エクスポートオプション</summary>
    [ObservableProperty]
    private ExportOptions _exportOptions = new();

    /// <summary>プログレス値</summary>
    [ObservableProperty]
    private int _progressValue;

    /// <summary>プログレス最大値</summary>
    [ObservableProperty]
    private int _progressMaximum = 100;

    /// <summary>ステータスメッセージ</summary>
    [ObservableProperty]
    private string _statusMessage = "処理待ち";

    /// <summary>エクスポート中か</summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ExportCommand))]
    [NotifyCanExecuteChangedFor(nameof(CancelExportCommand))]
    private bool _isExporting;

    /// <summary>図面ファイルがあるか</summary>
    [ObservableProperty]
    private bool _hasDrawingFiles;

    /// <summary>パーツ/アセンブリファイルがあるか</summary>
    [ObservableProperty]
    private bool _hasPartFiles;

    /// <summary>ファイルがあるか</summary>
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(ExportCommand))]
    private bool _hasFiles;

    /// <summary>選択中のサブフォルダリスト</summary>
    [ObservableProperty]
    private ObservableCollection<string> _subFolders = [];

    /// <summary>ZIPサブフォルダリスト</summary>
    [ObservableProperty]
    private ObservableCollection<string> _zipSubFolders = [];

    #endregion

    #region Commands

    /// <summary>ファイル追加コマンド</summary>
    [RelayCommand]
    private void AddFiles()
    {
        var dialog = new OpenFileDialog
        {
            Multiselect = true,
            Filter = "CADデータファイル|*.SLDDRW;*.SLDPRT;*.SLDASM|すべてのファイル|*.*"
        };

        if (dialog.ShowDialog() == true)
        {
            foreach (var file in dialog.FileNames)
            {
                AddFileIfNotExists(file);
            }
            UpdateFileFlags();
        }
    }

    /// <summary>選択ファイル削除コマンド</summary>
    [RelayCommand]
    private void RemoveSelectedFiles()
    {
        var selectedItems = FileList.Where(f => f.IsSelected).ToList();
        foreach (var item in selectedItems)
        {
            FileList.Remove(item);
        }
        UpdateFileFlags();
    }

    /// <summary>全ファイルクリアコマンド</summary>
    [RelayCommand]
    private void ClearFiles()
    {
        FileList.Clear();
        UpdateFileFlags();
    }

    /// <summary>エクスポートコマンド</summary>
    [RelayCommand(CanExecute = nameof(CanExport))]
    private async Task ExportAsync()
    {
        if (!ValidateOptions())
            return;

        IsExporting = true;
        _cancellationTokenSource = new CancellationTokenSource();

        var progress = new Progress<ExportProgress>(p =>
        {
            ProgressValue = p.Current;
            ProgressMaximum = p.Total;
            StatusMessage = p.StatusMessage;
        });

        try
        {
            var result = await _exportService.ExportAsync(
                FileList,
                ExportOptions,
                progress,
                _cancellationTokenSource.Token);

            if (result.WasCancelled)
            {
                StatusMessage = "処理がキャンセルされました";
            }
            else if (result.IsSuccess)
            {
                StatusMessage = $"完了: {result.SuccessCount}件のファイルをエクスポートしました";
            }
            else
            {
                StatusMessage = $"完了: {result.SuccessCount}件成功、{result.FailedCount}件失敗";
                if (result.Errors.Count > 0)
                {
                    MessageBox.Show(
                        string.Join("\n", result.Errors),
                        "エラー",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
        }
        catch (Exception ex)
        {
            StatusMessage = $"エラー: {ex.Message}";
            MessageBox.Show(ex.Message, "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsExporting = false;
            ProgressValue = 0;
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    }

    private bool CanExport() => HasFiles && !IsExporting;

    /// <summary>キャンセルコマンド</summary>
    [RelayCommand(CanExecute = nameof(CanCancel))]
    private void CancelExport()
    {
        var result = MessageBox.Show(
            "処理を中断しますか？",
            "確認",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (result == MessageBoxResult.Yes)
        {
            _cancellationTokenSource?.Cancel();
            StatusMessage = "キャンセル中...";
        }
    }

    private bool CanCancel() => IsExporting;

    /// <summary>出力フォルダ参照コマンド</summary>
    [RelayCommand]
    private void BrowseOutputFolder()
    {
        var path = SelectFolder();
        if (!string.IsNullOrEmpty(path))
        {
            ExportOptions.CustomFolderPath = path;
        }
    }

    /// <summary>ZIP出力フォルダ参照コマンド</summary>
    [RelayCommand]
    private void BrowseZipFolder()
    {
        var path = SelectFolder();
        if (!string.IsNullOrEmpty(path))
        {
            ExportOptions.ZipCustomFolderPath = path;
        }
    }

    #endregion

    #region IDropTarget Implementation

    public void DragOver(IDropInfo dropInfo)
    {
        var files = GetDroppedFiles(dropInfo);
        if (files.Any())
        {
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            dropInfo.Effects = DragDropEffects.Copy;
        }
    }

    public void Drop(IDropInfo dropInfo)
    {
        var files = GetDroppedFiles(dropInfo);
        foreach (var file in files)
        {
            AddFileIfNotExists(file);
        }
        UpdateFileFlags();
    }

    private static IEnumerable<string> GetDroppedFiles(IDropInfo dropInfo)
    {
        if (dropInfo.Data is DataObject dataObject && dataObject.ContainsFileDropList())
        {
            var files = dataObject.GetFileDropList();
            if (files != null)
            {
                return files.Cast<string>()
                    .Where(f => IsValidCadFile(f));
            }
        }
        return [];
    }

    private static bool IsValidCadFile(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToUpperInvariant();
        return ext is ".SLDDRW" or ".SLDPRT" or ".SLDASM";
    }

    #endregion

    #region Helper Methods

    private void AddFileIfNotExists(string filePath)
    {
        if (!FileList.Any(f => f.FullPath.Equals(filePath, StringComparison.OrdinalIgnoreCase)))
        {
            FileList.Add(CadFileItem.FromPath(filePath));
        }
    }

    private void UpdateFileFlags()
    {
        HasDrawingFiles = FileList.Any(f => f.FileType == CadFileType.Drawing);
        HasPartFiles = FileList.Any(f => f.FileType is CadFileType.Part or CadFileType.Assembly);
        HasFiles = FileList.Count > 0;

        // サブフォルダリストの更新
        UpdateSubFolders();
    }

    private void UpdateSubFolders()
    {
        SubFolders.Clear();
        ZipSubFolders.Clear();

        if (FileList.Count == 0) return;

        var firstFile = FileList.First();
        var dir = Path.GetDirectoryName(firstFile.FullPath);

        if (!string.IsNullOrEmpty(dir) && Directory.Exists(dir))
        {
            var folders = Directory.GetDirectories(dir)
                .Select(d => Path.GetFileName(d))
                .Where(d => !string.IsNullOrEmpty(d));

            foreach (var folder in folders)
            {
                SubFolders.Add(folder!);
                ZipSubFolders.Add(folder!);
            }
        }
    }

    private bool ValidateOptions()
    {
        // 少なくとも1つの出力フォーマットが選択されているか
        var hasDrawingFormat = ExportOptions.ExportPdf || ExportOptions.ExportDxf;
        var hasPartFormat = ExportOptions.ExportIgs || ExportOptions.ExportStep || ExportOptions.Export3mf;

        if (HasDrawingFiles && !hasDrawingFormat && HasPartFiles && !hasPartFormat)
        {
            MessageBox.Show("出力フォーマットを選択してください", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        // カスタムフォルダが選択されている場合、パスが設定されているか
        if (ExportOptions.OutputFolderOption == FolderOption.CustomFolder &&
            string.IsNullOrEmpty(ExportOptions.CustomFolderPath))
        {
            MessageBox.Show("出力フォルダを選択してください", "確認", MessageBoxButton.OK, MessageBoxImage.Warning);
            return false;
        }

        return true;
    }

    private static string? SelectFolder()
    {
        using var dialog = new CommonOpenFileDialog
        {
            IsFolderPicker = true
        };

        return dialog.ShowDialog() == CommonFileDialogResult.Ok ? dialog.FileName : null;
    }

    #endregion
}
