namespace CADExportTool.Core.Enums;

/// <summary>
/// エクスポートフォーマット
/// </summary>
public enum ExportFormat
{
    /// <summary>PDF形式 (図面用)</summary>
    Pdf,

    /// <summary>DXF形式 (図面用)</summary>
    Dxf,

    /// <summary>IGES形式 (パーツ/アセンブリ用)</summary>
    Igs,

    /// <summary>STEP形式 (パーツ/アセンブリ用)</summary>
    Step,

    /// <summary>3MF形式 (パーツ/アセンブリ用)</summary>
    ThreeMf
}
