namespace CADExportTool.Core.Models;

/// <summary>
/// エクスポート結果
/// </summary>
public class ExportResult
{
    /// <summary>成功したか</summary>
    public bool IsSuccess { get; set; }

    /// <summary>処理したファイル数</summary>
    public int ProcessedCount { get; set; }

    /// <summary>成功したファイル数</summary>
    public int SuccessCount { get; set; }

    /// <summary>失敗したファイル数</summary>
    public int FailedCount { get; set; }

    /// <summary>出力されたファイルパスのリスト</summary>
    public List<string> OutputFiles { get; set; } = [];

    /// <summary>エラーメッセージのリスト</summary>
    public List<string> Errors { get; set; } = [];

    /// <summary>キャンセルされたか</summary>
    public bool WasCancelled { get; set; }
}
