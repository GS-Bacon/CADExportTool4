namespace CADExportTool.Core.Enums;

/// <summary>
/// CADファイルの種類
/// </summary>
public enum CadFileType
{
    /// <summary>不明</summary>
    Unknown,

    /// <summary>図面ファイル (.SLDDRW)</summary>
    Drawing,

    /// <summary>パーツファイル (.SLDPRT)</summary>
    Part,

    /// <summary>アセンブリファイル (.SLDASM)</summary>
    Assembly
}
