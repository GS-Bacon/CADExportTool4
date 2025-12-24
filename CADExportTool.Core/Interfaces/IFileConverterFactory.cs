using CADExportTool.Core.Enums;

namespace CADExportTool.Core.Interfaces;

/// <summary>
/// ファイルコンバーターのファクトリー
/// </summary>
public interface IFileConverterFactory
{
    /// <summary>
    /// ファイルタイプに対応するコンバーターを取得
    /// </summary>
    /// <param name="fileType">CADファイルタイプ</param>
    /// <returns>対応するコンバーター</returns>
    /// <exception cref="NotSupportedException">サポートされていないファイルタイプの場合</exception>
    IFileConverter GetConverter(CadFileType fileType);
}
