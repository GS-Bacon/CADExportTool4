using CADExportTool.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CADExportTool.Core.Models;

/// <summary>
/// エクスポートオプション
/// </summary>
public partial class ExportOptions : ObservableObject
{
    // 図面オプション
    [ObservableProperty]
    private bool _exportPdf;

    [ObservableProperty]
    private bool _exportDxf;

    // パーツ/アセンブリオプション
    [ObservableProperty]
    private bool _exportIgs;

    [ObservableProperty]
    private bool _exportStep;

    [ObservableProperty]
    private bool _export3mf;

    // 出力フォルダオプション
    [ObservableProperty]
    private FolderOption _outputFolderOption = FolderOption.SameFolder;

    [ObservableProperty]
    private string _selectedSubFolder = string.Empty;

    [ObservableProperty]
    private string _customFolderPath = string.Empty;

    // ZIPオプション
    [ObservableProperty]
    private bool _createZip;

    [ObservableProperty]
    private FolderOption _zipFolderOption = FolderOption.SameFolder;

    [ObservableProperty]
    private string _zipSubFolder = string.Empty;

    [ObservableProperty]
    private string _zipCustomFolderPath = string.Empty;

    [ObservableProperty]
    private bool _createZipSubFolder;

    // その他オプション
    [ObservableProperty]
    private bool _createThumbnail;

    /// <summary>
    /// 図面用の出力フォーマットがあるか
    /// </summary>
    public bool HasDrawingFormats => ExportPdf || ExportDxf;

    /// <summary>
    /// パーツ/アセンブリ用の出力フォーマットがあるか
    /// </summary>
    public bool HasPartFormats => ExportIgs || ExportStep || Export3mf;
}
