using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;

namespace CADExportTool.Services.Converters;

/// <summary>
/// パーツコンバーター
/// </summary>
public class PartConverter : BaseConverter
{
    /// <inheritdoc/>
    public override CadFileType SupportedFileType => CadFileType.Part;

    /// <inheritdoc/>
    public override IReadOnlyList<ExportFormat> SupportedFormats => [ExportFormat.Igs, ExportFormat.Step, ExportFormat.ThreeMf];

    /// <inheritdoc/>
    protected override Task<object?> OpenDocumentAsync(ISolidWorksService solidWorksService, string filePath)
    {
        return solidWorksService.OpenPartAsync(filePath);
    }
}
