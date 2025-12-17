using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;

namespace CADExportTool.Services.Converters;

/// <summary>
/// アセンブリコンバーター
/// </summary>
public class AssemblyConverter : BaseConverter
{
    /// <inheritdoc/>
    public override CadFileType SupportedFileType => CadFileType.Assembly;

    /// <inheritdoc/>
    public override IReadOnlyList<ExportFormat> SupportedFormats => [ExportFormat.Igs, ExportFormat.Step, ExportFormat.ThreeMf];

    /// <inheritdoc/>
    protected override Task<object?> OpenDocumentAsync(ISolidWorksService solidWorksService, string filePath)
    {
        return solidWorksService.OpenAssemblyAsync(filePath);
    }
}
