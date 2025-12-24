namespace CADExportTool.Core.Interfaces;

/// <summary>
/// 更新チェック結果
/// </summary>
public record UpdateCheckResult(
    bool IsUpdateAvailable,
    string? LatestVersion,
    string? CurrentVersion,
    string? DownloadUrl,
    string? ReleaseNotes);

/// <summary>
/// アプリケーション更新サービス
/// </summary>
public interface IUpdateService
{
    /// <summary>
    /// 新しいバージョンが利用可能かチェック
    /// </summary>
    Task<UpdateCheckResult> CheckForUpdateAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 更新をダウンロードしてインストール
    /// </summary>
    Task<bool> DownloadAndInstallUpdateAsync(
        string downloadUrl,
        IProgress<int>? progress = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// 現在のバージョンを取得
    /// </summary>
    string GetCurrentVersion();
}
