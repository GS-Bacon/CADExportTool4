using CADExportTool.Core.Enums;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CADExportTool.Core.Models;

/// <summary>
/// CADファイルアイテム
/// </summary>
public partial class CadFileItem : ObservableObject
{
    [ObservableProperty]
    private string _fileName = string.Empty;

    [ObservableProperty]
    private string _fullPath = string.Empty;

    [ObservableProperty]
    private CadFileType _fileType;

    [ObservableProperty]
    private bool _isSelected;

    /// <summary>
    /// ファイルパスからCadFileItemを作成
    /// </summary>
    public static CadFileItem FromPath(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToUpperInvariant();
        return new CadFileItem
        {
            FileName = Path.GetFileName(filePath),
            FullPath = filePath,
            FileType = GetFileType(extension)
        };
    }

    /// <summary>
    /// 拡張子からファイルタイプを判定
    /// </summary>
    public static CadFileType GetFileType(string extension) => extension.ToUpperInvariant() switch
    {
        ".SLDDRW" => CadFileType.Drawing,
        ".SLDPRT" => CadFileType.Part,
        ".SLDASM" => CadFileType.Assembly,
        _ => CadFileType.Unknown
    };
}
