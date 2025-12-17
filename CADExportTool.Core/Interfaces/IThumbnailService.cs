namespace CADExportTool.Core.Interfaces;

/// <summary>
/// サムネイル取得サービス
/// </summary>
public interface IThumbnailService
{
    /// <summary>
    /// CADファイルからサムネイルを取得して保存
    /// </summary>
    /// <param name="filePath">CADファイルパス</param>
    /// <param name="outputFolder">出力フォルダ</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>出力ファイルパス (失敗時はnull)</returns>
    Task<string?> ExtractThumbnailAsync(
        string filePath,
        string outputFolder,
        CancellationToken cancellationToken = default);
}
