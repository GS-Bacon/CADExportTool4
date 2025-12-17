using CADExportTool.Core.Enums;

namespace CADExportTool.Core.Interfaces;

/// <summary>
/// ファイルコンバーター
/// </summary>
public interface IFileConverter
{
    /// <summary>対応するファイルタイプ</summary>
    CadFileType SupportedFileType { get; }

    /// <summary>対応する出力フォーマット</summary>
    IReadOnlyList<ExportFormat> SupportedFormats { get; }

    /// <summary>
    /// ファイルを変換
    /// </summary>
    /// <param name="solidWorksService">SolidWorksサービス</param>
    /// <param name="filePath">入力ファイルパス</param>
    /// <param name="outputFolder">出力フォルダ</param>
    /// <param name="format">出力フォーマット</param>
    /// <param name="cancellationToken">キャンセルトークン</param>
    /// <returns>出力ファイルパス (失敗時はnull)</returns>
    Task<string?> ConvertAsync(
        ISolidWorksService solidWorksService,
        string filePath,
        string outputFolder,
        ExportFormat format,
        CancellationToken cancellationToken = default);
}
