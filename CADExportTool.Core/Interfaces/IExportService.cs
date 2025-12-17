using CADExportTool.Core.Models;

namespace CADExportTool.Core.Interfaces;

/// <summary>
/// エクスポートサービス
/// </summary>
public interface IExportService
{
    /// <summary>
    /// エクスポートを実行
    /// </summary>
    /// <param name="files">エクスポート対象のファイルリスト</param>
    /// <param name="options">エクスポートオプション</param>
    /// <param name="progress">進捗報告</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>エクスポート結果</returns>
    Task<ExportResult> ExportAsync(
        IEnumerable<CadFileItem> files,
        ExportOptions options,
        IProgress<ExportProgress>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 総タスク数を計算
    /// </summary>
    int CalculateTotalTasks(IEnumerable<CadFileItem> files, ExportOptions options);
}
