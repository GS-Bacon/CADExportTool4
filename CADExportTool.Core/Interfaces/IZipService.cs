namespace CADExportTool.Core.Interfaces;

/// <summary>
/// ZIP圧縮サービス
/// </summary>
public interface IZipService
{
    /// <summary>
    /// ファイルをZIP圧縮
    /// </summary>
    /// <param name="sourceFiles">圧縮するファイルのリスト</param>
    /// <param name="outputPath">出力ZIPファイルパス</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>成功したか</returns>
    Task<bool> CreateZipAsync(
        IEnumerable<string> sourceFiles,
        string outputPath,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// フォルダをZIP圧縮
    /// </summary>
    /// <param name="sourceFolder">圧縮するフォルダ</param>
    /// <param name="outputPath">出力ZIPファイルパス</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>成功したか</returns>
    Task<bool> CreateZipFromFolderAsync(
        string sourceFolder,
        string outputPath,
        CancellationToken cancellationToken = default);
}
