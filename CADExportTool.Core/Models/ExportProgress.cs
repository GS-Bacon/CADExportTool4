namespace CADExportTool.Core.Models;

/// <summary>
/// エクスポート進捗情報
/// </summary>
public class ExportProgress
{
    /// <summary>現在の処理数</summary>
    public int Current { get; set; }

    /// <summary>総処理数</summary>
    public int Total { get; set; }

    /// <summary>現在処理中のファイル名</summary>
    public string CurrentFileName { get; set; } = string.Empty;

    /// <summary>ステータスメッセージ</summary>
    public string StatusMessage { get; set; } = string.Empty;

    /// <summary>進捗率 (0-100)</summary>
    public int PercentComplete => Total > 0 ? (int)((double)Current / Total * 100) : 0;
}
