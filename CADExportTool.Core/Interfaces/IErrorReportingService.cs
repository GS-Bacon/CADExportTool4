namespace CADExportTool.Core.Interfaces;

/// <summary>
/// エラー報告結果
/// </summary>
public record ErrorReportResult(bool IsSuccess, string? IssueUrl, string? ErrorMessage);

/// <summary>
/// GitHub Issuesへのエラー自動報告サービス
/// </summary>
public interface IErrorReportingService
{
    /// <summary>
    /// エラー報告が有効かどうか
    /// </summary>
    bool IsEnabled { get; set; }

    /// <summary>
    /// エラーをGitHub Issuesに報告
    /// </summary>
    Task<ErrorReportResult> ReportErrorAsync(
        Exception exception,
        string? additionalContext = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// ユーザーに報告許可を求める
    /// </summary>
    Task<bool> AskForPermissionAsync(Exception exception);
}
