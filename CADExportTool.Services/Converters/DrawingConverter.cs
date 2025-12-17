using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;

namespace CADExportTool.Services.Converters;

/// <summary>
/// 図面コンバーター
/// </summary>
public class DrawingConverter : BaseConverter
{
    /// <inheritdoc/>
    public override CadFileType SupportedFileType => CadFileType.Drawing;

    /// <inheritdoc/>
    public override IReadOnlyList<ExportFormat> SupportedFormats => [ExportFormat.Pdf, ExportFormat.Dxf];

    /// <inheritdoc/>
    protected override Task<object?> OpenDocumentAsync(ISolidWorksService solidWorksService, string filePath)
    {
        return solidWorksService.OpenDrawingAsync(filePath);
    }
}
