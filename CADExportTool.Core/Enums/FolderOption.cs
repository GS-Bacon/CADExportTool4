namespace CADExportTool.Core.Enums;

/// <summary>
/// 出力先フォルダオプション
/// </summary>
public enum FolderOption
{
    /// <summary>元ファイルと同じフォルダ</summary>
    SameFolder,

    /// <summary>元ファイルの直下サブフォルダ</summary>
    SubFolder,

    /// <summary>指定したフォルダ</summary>
    CustomFolder
}
