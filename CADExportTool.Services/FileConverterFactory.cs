using CADExportTool.Core.Enums;
using CADExportTool.Core.Interfaces;
using CADExportTool.Services.Converters;

namespace CADExportTool.Services;

/// <summary>
/// ファイルコンバーターのファクトリー実装
/// </summary>
public class FileConverterFactory : IFileConverterFactory
{
    private readonly Dictionary<CadFileType, IFileConverter> _converters;

    public FileConverterFactory()
    {
        _converters = new Dictionary<CadFileType, IFileConverter>
        {
            { CadFileType.Drawing, new DrawingConverter() },
            { CadFileType.Part, new PartConverter() },
            { CadFileType.Assembly, new AssemblyConverter() }
        };
    }

    /// <inheritdoc/>
    public IFileConverter GetConverter(CadFileType fileType)
    {
        if (_converters.TryGetValue(fileType, out var converter))
        {
            return converter;
        }

        throw new NotSupportedException($"サポートされていないファイルタイプです: {fileType}");
    }
}
